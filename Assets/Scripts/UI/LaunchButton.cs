using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchButton : Buttons, IChoosable
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
        SceneManager.LoadScene("Base");
    }

    public void Desactivate()
    {
        _changedText.color = _desactivatedColor;
    }
}
