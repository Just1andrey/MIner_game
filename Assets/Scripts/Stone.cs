using UnityEngine;

public class Stone : MonoBehaviour
{
    float y;
    [Header("лНФМН ХГЛЕМЪРЭ: ")]

    [SerializeField] float speed;
    [SerializeField] int hp;

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
        

    } //лсвюкяъ мюд щрхл ме гмюлемхрши днканъыеп бкюдхякюб.

     void OnMouseDown()
     {
        hp -= attack_of_stone.Instantate.damage;
        if(hp <= 0)
        {
            Properties.instance.AddMoney();
            Properties.instance.AddExp();
            Destroy(gameObject);
            
        }
     }
}
