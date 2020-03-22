using System.Collections.Generic;
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
    private Button _btn;

    [SerializeField]
    private string _type;

    private List<Upgrade> _availableUpgrades;
    private int _level;

    private TransitionSaver _transitionSaver;



    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        _availableUpgrades = _transitionSaver.RecoverUpgrades(_type);
        _level = _transitionSaver.RecoverUpgradesNumber(_type);
        DisplayChange();
    }


    public void Upgrade()
    {
        if (_level < _availableUpgrades.Count)
        {
            if(_transitionSaver.GetScrapCount() >= _availableUpgrades[_level].GetCost())
            {
                _transitionSaver.RemoveScrap(_availableUpgrades[_level].GetCost());
                _level++;
                _transitionSaver.UpgradeBench(_type);
                DisplayChange();
            }
        }
    }


    private void DisplayChange()
    {
        _mainText.text = _availableUpgrades[_level].GetDescriptionText();
        _upgradeText.text = "Upgrade for " + _availableUpgrades[_level].GetCost();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _ui.SetActive(true);
        DisplayChange();
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(delegate { this.Upgrade(); });
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _ui.SetActive(false);
    }
}