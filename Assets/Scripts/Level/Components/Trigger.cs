using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _triggeredObjects;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            foreach (GameObject current in _triggeredObjects)
                current.GetComponent<IActivable>().Activate();
        }
    }
}