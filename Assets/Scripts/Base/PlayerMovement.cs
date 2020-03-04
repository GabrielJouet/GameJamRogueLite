using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<TransitionSaver>().SetScrapCount(50);
            FindObjectOfType<TransitionSaver>().LoadBase();
        }
    }
}
