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
    private bool start_spawning = true ;

    [Header("Родитель и ребёнок камень")]
    public GameObject stone;
    private GameObject Parent_of_stone;

    public static GameManager Instantate;

    public float posy;
    void Start ()
    {
        Invoke("spawn", 2.0f);

        Instantate = GetComponent<GameManager>();

        Parent_of_stone = GameObject.Find("Parent_of_stone");
    }

    void Update()
    {
        if(Parent_of_stone.transform.childCount == 10)
        {
            start_spawning = false;
            CancelInvoke();
        }
        else if (Parent_of_stone.transform.childCount == 5 && !IsInvoking("spawn"))
        {
            start_spawning = true;
            Invoke("spawn", 2.0f);
        }
        else if (!IsInvoking("spawn") && start_spawning)
        {

            Invoke("spawn", 2.0f);
        }
    }

    void spawn()
    {
        GameObject stone;

        stone = Instantiate(prefabeStone);

        float x = Mathf.Floor(Random.Range(0.17f , 2.35f - 0.17f) / 0.17f) * 0.17f + 0.17f * 0.5f;

        float y = Mathf.Floor(-Random.Range(0.17f, 1.70f - 0.17f) / 0.17f) * 0.17f - 0.17f * 0.5f; ;

        posy = y;

        stone.transform.position = new Vector3(x, y1);

        stone.transform.parent = Parent_of_stone.transform;

    }
}
