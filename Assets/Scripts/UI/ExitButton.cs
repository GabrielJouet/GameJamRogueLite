using UnityEngine;
using UnityEngine.UI;

public class ExitButton : Buttons, IChoosable
{
    [Header("Buttons-related")]
    [SerializeField]
    private Color _activateColor;

    [SerializeField]
    private Color _desactivatedColor;

    [SerializeField]
    private Text _changedText;


    public void Activate()
    {
        _audioSource.clip = _audioPlayed;
        _audioSource.Play();
        _changedText.color = _activateColor;
    }

    public void Choose()
    {
        Application.Quit();
    }

    public void Desactivate()
    {
        _changedText.color = _desactivatedColor;
    }
}