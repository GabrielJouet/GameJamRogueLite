using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceSpawner : MonoBehaviour
{
    [SerializeField]
    private int _interfaceId;
    private bool _canOpenInterface = false;

    [SerializeField]
    private GameObject _interfacePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _canOpenInterface)
        {
            switch (_interfaceId)
            {
                case 0: //FireCamp
                    Instantiate(_interfacePrefab, new Vector3(0f,0f,0f), Quaternion.identity);
                    break;
                case 1: //Well
                    Instantiate(_interfacePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    break;
                case 2: //ArmorStand
                    break;
                case 3: //Backpack
                    break;
                case 4: //Spells
                    Instantiate(_interfacePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    break;
                case 5: // ShoesStorage
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _canOpenInterface = true;
        }
        
    }
}
