using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private int _maxHealth = 20;
    private int _currentHealth;

    private int _maxStamina = 40;
    private int _currentStamina;
    private int _dashCost = 5;
    private float _staminaRegenTime = 2f;
    private float _timeSinceLastRegen;
    private int _staminaRegenAmount = 5;



    [SerializeField]
    private HealthBarScript _healthBar;
    [SerializeField]
    private StaminaBarScript _staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _currentStamina = _maxStamina;
        _staminaBar.SetMaxStamina(_maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //to test the health bar
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            LooseStamina(_dashCost);
        }

        StaminaRegen();
    }

    private void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
    }

    private void LooseStamina(int staminaCost)
    {
        _currentStamina -= staminaCost;
        _staminaBar.SetStamina(_currentStamina);
    }

    private void StaminaRegen()
    {
        _timeSinceLastRegen += Time.deltaTime;
        if (_timeSinceLastRegen >= _staminaRegenTime)
        {
            _timeSinceLastRegen = 0;
            _currentStamina += _staminaRegenAmount;
            _staminaBar.SetStamina(_currentStamina);
            if (_currentStamina > _maxStamina)
            {
                _staminaBar.SetStamina(_maxStamina);
            }
        }
    }
}
