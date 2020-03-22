using UnityEngine;

public class Well : MonoBehaviour
{
    private bool _actived = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(_actived && collision.GetComponent<PlayerMovement>())

    }
}
