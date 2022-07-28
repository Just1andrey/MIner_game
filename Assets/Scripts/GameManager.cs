using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("зона спавна камней")]
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] GameObject prefabeStone;
    [SerializeField] float y1;

    [Header("Родитель и ребёнок камень")]
    public GameObject stone;
    private GameObject Parent_of_stone;

    public static GameManager Instantate;

    public float posy;
    void Start()
    {
        InvokeRepeating("spawn", 3, 3);

        Instantate = this;

        Parent_of_stone = GameObject.Find("Parent_of_stone");
    }

    void Update()
    {

    }

    void spawn()
    {
        GameObject stone;

        stone = Instantiate(prefabeStone);
        
        float x = Random.Range(left.position.x + left.localScale.x, right.position.x - right.localScale.x);

        float y = Random.Range(up.position.y + up.localScale.y, down.position.y - down.localScale.y);

        posy = y;

        stone.transform.position = new Vector3(x, y1);

        stone.transform.parent = Parent_of_stone.transform;
    }
}
