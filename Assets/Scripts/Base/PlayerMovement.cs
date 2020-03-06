using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    private TransitionSaver _transitionSaver;


    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
    }


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


        if (Input.GetKeyDown(KeyCode.R) && _transitionSaver.GetCanTeleport())
        {
            _transitionSaver.LoadBase();
            _transitionSaver.SetCanTeleport(false);
        }
    }
}
