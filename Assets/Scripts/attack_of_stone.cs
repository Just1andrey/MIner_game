using UnityEngine;

public class attack_of_stone : MonoBehaviour
{
    public int damage;
    public static attack_of_stone Instantate;
    void Start()
    {
        Instantate = GetComponent<attack_of_stone>();
    }

    void Update()
    {
        
    }
}
