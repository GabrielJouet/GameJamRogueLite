using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour, IChoosable
{
    [SerializeField]
    private Color _activateColor;

    [SerializeField]
    private Color _desactivatedColor;

    [SerializeField]
    private Text _changedText;


    public void Activate()
    {
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