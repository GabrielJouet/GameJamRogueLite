using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarScript : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private Image _fill;

    public void SetMaxStamina(int stamina)
    {
        _slider.maxValue = stamina;
        _slider.value = stamina;
        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetStamina(int stamina)
    {
        _slider.value = stamina;

        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}
