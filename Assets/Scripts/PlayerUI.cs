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

    [SerializeField]
    private Text _scrapCount;



    public void SetMaxHealth(float health)
    {
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(_healthSlider.normalizedValue);
    }

    public void SetMaxStamina(float stamina)
    {
        _staminaSlider.maxValue = stamina;
        _staminaSlider.value = stamina;
        _staminaFill.color = _staminaGradient.Evaluate(1f);
    }

    public void SetStamina(float stamina)
    {
        _staminaSlider.value = stamina;
        _staminaFill.color = _staminaGradient.Evaluate(_staminaSlider.normalizedValue);
    }

    public void SetScrap(int other)
    {
        _scrapCount.text = other.ToString();
    }
}