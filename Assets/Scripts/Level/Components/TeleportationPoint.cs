using UnityEngine;

public class TeleportationPoint : MonoBehaviour
{
    private bool _active = true;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() && _active)
        {
            _active = false;
            FindObjectOfType<TransitionSaver>().SetCanTeleport(true);
        }
    }
}
