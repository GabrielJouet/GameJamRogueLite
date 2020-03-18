using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float _speed = 5000;
    [SerializeField]
    private int _armor;
    [SerializeField]
    private int _maxStorage;

    [SerializeField]
    private Rigidbody2D _rigidBody;

    private int _dashCost = 5;
    private int _staminaRegenAmount = 2;
    private float _staminaRegenTime = 2f;
    private bool _staminaCR_running = false;
    private IEnumerator _coStamina;

    private TransitionSaver _transitionSaver;
    private PlayerUI _playerUI;
    private string _currentScene;


    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        _currentScene = SceneManager.GetActiveScene().name;
        _coStamina = RegenMana();
        if (_currentScene == "Dungeon")
        {
            _playerUI = FindObjectOfType<PlayerUI>();
            _currentHealth = _maxHealth;
            _playerUI.SetMaxHealth(_currentHealth);
            _currentStamina = _maxStamina;
            _playerUI.SetMaxStamina(_maxStamina);
        }
    }


    private void Update()
    {
        _rigidBody.AddForce(Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1) * _speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.R) && _transitionSaver.GetCanTeleport())
        {
            _transitionSaver.LoadBase();
            _transitionSaver.SetCanTeleport(false);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //to test the health bar
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.X)) //to test the staminaBar
        {
            LooseStamina(_dashCost);
        }
    }

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _playerUI.SetHealth(_currentHealth);
    }

    private void LooseStamina(int staminaCost)
    {
        Debug.Log(_staminaCR_running);
        _currentStamina -= staminaCost;
        _playerUI.SetStamina(_currentStamina);
        if (_staminaCR_running == true)
        {
            StopCoroutine(_coStamina);
            _staminaCR_running = false;
            StartCoroutine(_coStamina);
        }
        else
        {
            StartCoroutine(_coStamina);
        }
    }

    private IEnumerator RegenMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(_staminaRegenTime);
            _staminaCR_running = true;
            if (_currentStamina + _staminaRegenAmount >= _maxStamina)
            {
                _currentStamina = _maxStamina;
                _playerUI.SetStamina(_currentStamina);
                _staminaCR_running = false;
                StopCoroutine(_coStamina);
            }
            else
            {
                _currentStamina += _staminaRegenAmount;
                _playerUI.SetStamina(_currentStamina);
            }
        }
    }


    //--------------------------------Setter

    public void SetMaxHealth(int health) { _maxHealth = health; }
    public void SetMaxStamina(int stamina) { _maxStamina = stamina; }
    public void SetMaxStorage(int storage) { _maxStorage = storage; }
    public void SetArmor(int armor) { _armor = armor; }
    public void SetSpeedBoost(int speedBoost) { _speedBoost = speedBoost; }
}
