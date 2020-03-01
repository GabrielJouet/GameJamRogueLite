using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0)
        {
            transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * _speed);
        }
        if (verticalInput != 0)
        {
            transform.Translate(Vector2.up * verticalInput * Time.deltaTime * _speed);
        }
    }
}
