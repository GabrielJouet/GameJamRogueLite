using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private int _damage;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
            collision.GetComponent<PlayerMovement>().TakeDamage(_damage);
    }
}