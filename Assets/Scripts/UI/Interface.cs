using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Text _mainText;
    [SerializeField]
    private Text _upgradeText;

    [SerializeField]
    private GameObject _ui;

    [SerializeField]
    private string _type;

    [SerializeField]
    private Animator _buttonAnimator;

    [SerializeField]
    private string _finalText;

    private Upgrade _availableUpgrade;

    private TransitionSaver _transitionSaver;

    private bool _activated = false;
    private bool _noMoreUpgradesLeft = false;


    private void Update()
    {
        if (_activated && Input.GetKeyDown(KeyCode.E) && !_noMoreUpgradesLeft)
            Upgrade();
    }


    private void Upgrade()
    {
        if(_transitionSaver.GetScrapCount() >= _availableUpgrade.GetCost())
        {
            _buttonAnimator.SetTrigger("good");
            _transitionSaver.RemoveScrap(_availableUpgrade.GetCost());
            _transitionSaver.UpgradeBench(_type);

            RecoverUpgrade();
            DisplayChange();
        }
        else if (_transitionSaver.GetScrapCount() < _availableUpgrade.GetCost() || _noMoreUpgradesLeft)
            _buttonAnimator.SetTrigger("bad");
    }


    private void RecoverUpgrade()
    {
        if (_transitionSaver == null)
            _transitionSaver = FindObjectOfType<TransitionSaver>();

        _availableUpgrade = _transitionSaver.RecoverUpgrade(_type);

        _noMoreUpgradesLeft = _availableUpgrade == null;
    }


    private void DisplayChange()
    {
        if(!_noMoreUpgradesLeft)
        {
            _upgradeText.text = "Upgrade for " + _availableUpgrade.GetCost();
            _mainText.text = _availableUpgrade.GetDescriptionText();
        }
        else
        {
            _upgradeText.text = "Max level reached";
            _mainText.text = _finalText;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RecoverUpgrade();
        _activated = true;
        _ui.SetActive(true);
        DisplayChange();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _activated = false;
        _ui.SetActive(false);
    }
}