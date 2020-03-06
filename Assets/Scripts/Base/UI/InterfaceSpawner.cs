using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawner : MonoBehaviour
{
    private bool _canOpenInterface = false;
    private bool _interfaceIsOpen = false;

    [SerializeField]
    private GameObject _interface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canOpenInterface && !_interfaceIsOpen)
        {
            _interface.SetActive(true);
            _interfaceIsOpen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _canOpenInterface = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _interface.SetActive(false);
            _interfaceIsOpen = false;
            _canOpenInterface = false;
        }
    }
}
