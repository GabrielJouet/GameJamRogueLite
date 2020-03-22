using UnityEngine;

public class Scrap : MonoBehaviour
{
    [SerializeField]
    private int _value;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            collision.GetComponent<PlayerMovement>().CollectScrap(_value);
            gameObject.SetActive(false);
        }
    }
}