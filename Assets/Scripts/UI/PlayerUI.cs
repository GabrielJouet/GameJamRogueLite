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
    private Text _maxHealth;
    [SerializeField]
    private Text _health;

    [SerializeField]
    private Text _scrapCount;



    public void SetMaxHealth(float health)
    {
        _maxHealth.text = "/" + health;
        _health.text = health.ToString();

        _healthSlider.maxValue = health;
        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(1f);
    }


    public void SetHealth(float health)
    {
        _health.text = health.ToString();

        _healthSlider.value = health;
        _healthFill.color = _healthGradient.Evaluate(_healthSlider.normalizedValue);
    }


    public void SetScrap(int other)
    {
        _scrapCount.text = other.ToString();
    }
}