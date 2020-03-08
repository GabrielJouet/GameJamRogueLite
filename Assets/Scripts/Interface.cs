using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private Text _mainText;
    [SerializeField]
    private Text _boostText;
    [SerializeField]
    private Text _upgradeText;
    [SerializeField]
    private Button _upgradeButton;

    [SerializeField]
    private string _type;

    [SerializeField]
    private List<Upgrade> _availableUpgrades;
    private Upgrade _loadedUpgrade;

    private TransitionSaver _transitionSaver;

    private int _level = 0;

    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        _loadedUpgrade = _availableUpgrades[_transitionSaver.RecoverUpgradeBench(_type)];
        DisplayChange();
    }


    public void UpgradeArmor()
    {
        if(_level < _availableUpgrades.Count)
        {
            if(_transitionSaver.GetScrapCount() >= _loadedUpgrade.GetCost())
            {
                _level++;
                _transitionSaver.SetBenchUpgrade(_type);
                _loadedUpgrade = _availableUpgrades[_level];
                DisplayChange();
            }
        }
    }

    private void DisplayChange()
    {
        _mainText.text = _loadedUpgrade.GetDescriptionText();
        _boostText.text = _loadedUpgrade.GetBoost().ToString();
        _upgradeText.text = "Upgrade for " + _loadedUpgrade.GetCost();
    }
}