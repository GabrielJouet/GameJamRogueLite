using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private int _maxHealth;
    private int _health;
    [SerializeField]
    private float _maxStamina;
    private float _stamina;
    [SerializeField]
    private float _speed = 7500;
    private float _maxSpeed;
    [SerializeField]
    private float _armor;
    [SerializeField]
    private int _storageMalus;
    [SerializeField]
    private float _dashCost = 5;
    [SerializeField]
    private float _staminaRegenAmount = 2;
    [SerializeField]
    private float _staminaRegenTime = 2f;


    private int _scrapCount = 0;
    private bool _canMove = true;


    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigidBody;

    private bool _staminaCR_running = false;
    private IEnumerator _coStamina;

    private TransitionSaver _transitionSaver;
    private PlayerUI _playerUI;


    public void Initialize(TransitionSaver newSaver, PlayerUI newPlayerUI)
    {
        _transitionSaver = newSaver;
        _playerUI = newPlayerUI;

        _transitionSaver.ApplyPlayerStat(this);
    }


    private void Update()
    {
        if(_canMove)
        {
            _rigidBody.AddForce(Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1) * _speed * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.R) && _transitionSaver.GetCanTeleport())
            {
                _transitionSaver.LoadBase();
                _transitionSaver.SetCanTeleport(false);
            }
        }
    }


    private void TakeDamage(int damage)
    {
        _health -= damage;
        _playerUI.SetHealth(_health);
    }


    private void LooseStamina(int staminaCost)
    {
        _stamina -= staminaCost;
        _playerUI.SetStamina(_stamina);

        if (_staminaCR_running)
        {
            StopCoroutine(_coStamina);
            StartCoroutine(_coStamina);
        }
        else
            StartCoroutine(_coStamina);
    }


    private IEnumerator RegenStamina()
    {
        _staminaCR_running = true;

        while (_stamina < _maxStamina)
        {
            yield return new WaitForSeconds(_staminaRegenTime);

            _stamina += (_stamina + _staminaRegenAmount >= _maxStamina) ? 0 : _staminaRegenAmount;
            _playerUI.SetStamina(_stamina);
        }

        _staminaCR_running = false;
    }


    public void CollectScrap(int value)
    {
        _scrapCount += value;
        _speed = _maxSpeed - (_maxSpeed * (_scrapCount * _storageMalus) / 100f);
        _playerUI.SetScrap(_scrapCount);
    }


    public void AbandonScrap()
    {
        _scrapCount--;
        _speed = _maxSpeed - (_maxSpeed * (_scrapCount * _storageMalus) / 100f);
        _playerUI.SetScrap(_scrapCount);
    }


    //--------------------------------Setter
    public void SetMaxHealth(int health) 
    { 
        _maxHealth = health;
        _playerUI.SetMaxHealth(_maxHealth);
    }

    public void SetMaxStamina(int stamina) 
    { 
        _maxStamina = stamina;
        _playerUI.SetMaxStamina(_maxStamina);
    }

    public void SetStorageMalus(int storage) 
    {
        _storageMalus = storage; 
    }

    public void SetArmor(float armor) 
    { 
        _armor = armor; 
    }

    public void SetSpeed(float speed) 
    { 
        _speed = speed;
        _maxSpeed = _speed;
    }

    public void SetSupportBoost(float supportBoost) 
    {
        _staminaRegenAmount = supportBoost; 
    }

    public void SetCanMove(bool other) { _canMove = other; }
}