using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchButton : MonoBehaviour, IChoosable
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
        SceneManager.LoadScene("Base");
    }

    public void Desactivate()
    {
        _changedText.color = _desactivatedColor;
    }
}
