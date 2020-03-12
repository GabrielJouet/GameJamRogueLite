using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _interface;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _interface.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _interface.SetActive(false);
    }
}
