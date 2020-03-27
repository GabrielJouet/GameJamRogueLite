using UnityEngine;

public class SlowDown : MonoBehaviour
{
    [SerializeField]
    private float _speedMalus;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
            collision.GetComponent<PlayerMovement>().ModifySpeed(_speedMalus);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>())
            collision.GetComponent<PlayerMovement>().ResetSpeed();
    }
}