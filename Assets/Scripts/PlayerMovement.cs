using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _currentHealth;
    [SerializeField]
    private int _maxStamina;
    [SerializeField]
    private int _currentStamina;
    [SerializeField]
    private int _speedBoost;
    private float _speed = 2f;
    [SerializeField]
    private int _armor;
    [SerializeField]
    private int _maxStorage;

    private int _dashCost = 5;

    private TransitionSaver _transitionSaver;

    [SerializeField]
    private PlayerUI _playerUI;


    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        _currentHealth = _maxHealth;
        _currentStamina = _maxStamina;
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

    //--------------------------------Setter

    public void SetMaxHealth(int health) { _maxHealth = health; }
    public void SetMaxStamina(int stamina) { _maxStamina = stamina; }
    public void SetMaxStorage(int storage) { _maxStorage = storage; }
    public void SetArmor(int armor) { _armor = armor; }
    public void SetSpeedBoost(int speedBoost) { _speedBoost = speedBoost; }
}
