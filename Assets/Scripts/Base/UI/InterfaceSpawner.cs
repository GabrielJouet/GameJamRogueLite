using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawner : MonoBehaviour
{
    [SerializeField]
    private int _interfaceId;

    [SerializeField]
    private GameObject _campFireInterface;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (_interfaceId)
            {
                case 0:
                    Debug.Log("ok");
                    Instantiate(_campFireInterface);
                    break;
            }
        }
    }
}
