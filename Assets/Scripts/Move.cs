using UnityEngine;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
	private List<Transform> stones;
	private GameObject parent;
	private int[,] grid;
	private int w, h;
	private int cellSize;
	[SerializeField] private GameObject player;
	[SerializeField] private int playableWidth;
	[SerializeField] private int playableHeight;
	private List<Cell> path;
	private bool mouseWasPressed = false;
	
	public class Cell{
		public int x, y;
		private float costG = 1000000000;
		private float costH = 1000000000;
		private Cell parent = null;
		private List<Cell> neighbours = null;

		private int w, h;
		public Cell (int x, int y, int w, int h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}
		public int getX(){
			return x;
		}
		public int getY(){
			return y;
		}
		public void setCostG(float costG){
			this.costG = costG;
		}
		public void setCostH(float costH){
			this.costH = costH;
		}
		public float getCostF(){
			return costG + costH;
		}
		public float getCostH(){
			return costH;
		}
		public float getCostG(){
			return costG;
		}
		public void setParent(Cell parent){
			this.parent = parent;
		}
		public Cell getParent(){
			return parent;
		}
		public bool equals(Cell other){
			if (x == other.x && y == other.y){
			return true;
			}
			return false;
		}
		public List<Cell> getNeighbours(){
			if (neighbours != null)
				return neighbours;
			neighbours = new List<Cell>();
			for (int xOff = -1; xOff <= 1; xOff++){
				for (int yOff = -1; yOff <= 1; yOff++){
					if (x + xOff >= 0 && x + xOff < w && y + yOff >= 0 && y + yOff < h){
						neighbours.Add(new Cell(x + xOff, y + yOff, w , h));
					}
				}
			}
			return neighbours;
		}
		public float distSq(Cell other){
			return (x - other.x) * (x - other.x) + (y - other.y) * (y - other.y);
		}
		public Cell copy(){
			Cell copy = new Cell(x, y , w ,h);
			copy.setParent(parent);
			return copy;
		}
	}
	
	void Start(){
		parent = GameObject.Find("Parent_of_stone");
		cellSize = 17;
		w = playableWidth / cellSize;
		h = playableHeight / cellSize;
		
		grid = new int[w , h];
		UpdateStones();
		UpdateGrid();
		path = new List<Cell>();
	}
	
	void UpdateGrid(){
		for (int x = 0; x < w; x++){
			for (int y = 0; y < h; y++){
				grid[x , y] = 0;
			}
		}
		
		for (int i = 0; i < parent.transform.childCount; i++){

            if (stones[i].position.y > 0) 
			{
				continue;
			}

			int gridX = (int)(stones[i].position.x * 100.0f / cellSize);
			int gridY = (int)(-stones[i].position.y * 100.0f / cellSize);

			grid[gridX , gridY] = 1;
		}
	}
	
	void UpdateStones(){
		stones = new List<Transform>() ;
		for (int i = 0; i < parent.transform.childCount; i++){
			stones.Add(parent.transform.GetChild(i));
		}
	}
	
	void WhenMousePressed(){

		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.x *= 100;
		pos.y *= 100;

		if (pos.x >=0 && pos.y <=0 && pos.x < playableWidth && pos.y > -playableHeight)
		{
			UpdateStones();
			UpdateGrid();
			int playerGridX = (int)(player.transform.position.x * 100 / cellSize);
			int playerGridY = (int)(-player.transform.position.y * 100 / cellSize);
			int targetGridX = (int)(pos.x / cellSize);
			int targetGridY = (int)(-pos.y / cellSize);
			Cell start = new Cell(playerGridX, playerGridY,w,h);
			Cell end = new Cell(targetGridX, targetGridY,w,h);
			path = A_star(start, end);

			/*
			print(playerGridX);
			print(playerGridY);
			print(targetGridX);
			print(targetGridY);
			*/
		}
	}
	
	List<Cell> A_star(Cell start, Cell end){
		List<Cell> open = new List<Cell>();
		List<Cell> closed = new List<Cell>();
		start.setCostG(0);
		start.setCostH(0);
		open.Add(start);
		List<Cell> path = new List<Cell>();
  
		while (open.Count > 0){
			Cell minCostCell = null;
			foreach (Cell cell in open){
				if (minCostCell == null || cell.getCostF() < minCostCell.getCostF() || (cell.getCostF() == minCostCell.getCostF() && cell.getCostH() < minCostCell.getCostH())){
					minCostCell = cell;
				}
			}
			open.Remove(minCostCell);
			closed.Add(minCostCell);
    
			if (minCostCell.equals(end)){
				Cell pathCell = minCostCell;
				while (!pathCell.equals(start)){
					path.Add(pathCell);
					pathCell = pathCell.getParent().copy();
				}
				return path;
			}
			foreach (Cell cell in minCostCell.getNeighbours()){
				if (grid[cell.getX() ,cell.getY()] == 1){
					continue;
				}
				bool isClosed = false;
				foreach (Cell closedCell in closed){
					if (closedCell.equals(cell)){
						isClosed = true;
						break;
					}
				}
				if (isClosed){
					continue;
				}
				bool isOpen = false;
				Cell openC = null;
				foreach (Cell openCell in open){
					if (openCell.equals(cell)){
						isOpen = true;
						openC = openCell;
						break;
					}
				}
				if (!isOpen){
					cell.setCostG(minCostCell.getCostG() + minCostCell.distSq(cell));
					cell.setCostH(cell.distSq(end));
					cell.setParent(minCostCell);
					open.Add(cell);
				}
				else{
					float newCostF = minCostCell.getCostG() + minCostCell.distSq(openC) + openC.getCostH();
					if (newCostF < openC.getCostF()){
						openC.setCostG(minCostCell.getCostG() + minCostCell.distSq(openC));
						openC.setParent(minCostCell);
					}
				}
			}
		}
		return path;
	}
	
	void Update(){
		if (path.Count > 0)
		{
			Cell pathCell = path[path.Count - 1];
			Vector3 pathPos = new Vector3(pathCell.x / 100.0f * cellSize + cellSize / 100.0f * 0.5f, -pathCell.y / 100.0f * cellSize - cellSize / 100.0f * 0.5f);
			Vector3 velocity = (pathPos - player.transform.position).normalized * Time.deltaTime * 1.0f;
			player.transform.position += velocity;
			if ((pathPos - player.transform.position).sqrMagnitude < 0.001f){
				path.RemoveAt(path.Count - 1);
			}
		}
		if (Input.GetMouseButtonDown(0) && !mouseWasPressed){
			WhenMousePressed();
		}
		
		mouseWasPressed = Input.GetMouseButtonDown(0);
	}
}