using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Panel")]
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


    [Header("Death Panel")]
    [SerializeField]
    private GameObject _deathUI;
    [SerializeField]
    private Text _scrapCountOnDeath;
    [SerializeField]
    private Text _descriptionText;
    [SerializeField]
    private GameObject _winUI;
    [SerializeField]
    private GameObject _restartUI;


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


    public void ShowDeathPanel(bool dead, int scrapCount)
    {
        _deathUI.SetActive(true);
        _scrapCountOnDeath.text = "You gathered: " + scrapCount + " scraps";
        _descriptionText.text = dead ? "Don't cry, it's just a bad wound..." : "The journey only begins, now your skills will be truly tested...";

        _winUI.SetActive(!dead);
        _restartUI.SetActive(dead);
    }
}