using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Gradient _healthGradient;
    [SerializeField]
    private Image _healthFill;

    [SerializeField]
    private Slider _staminaSlider;
    [SerializeField]
    private Gradient _staminaGradient;
    [SerializeField]
    private Image _staminaFill;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMaxHealth(int health)
    {
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(_healthSlider.normalizedValue);
    }
}


