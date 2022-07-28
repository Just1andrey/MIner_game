using UnityEngine;

public class Stone : MonoBehaviour
{
    float y;
    [Header("����� ��������: ")]

    [SerializeField] float speed;
    public int hp = 3;

    void Start()
    {
        y = GameManager.Instantate.posy;
    }

     void FixedUpdate()
    {
        gameObject.transform.Translate(0, Time.fixedDeltaTime * -speed, 0);
        
        if (y >= gameObject.transform.position.y)
        {
            speed = 0;
        }
        

    } //������� ��� ���� �� ���������� ��������� ���������.

     void OnMouseDown()
     {

        hp -= attack_of_stone.Instantate.damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

     }
}
