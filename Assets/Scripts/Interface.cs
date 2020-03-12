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
    private Image _boostIcon;
    [SerializeField]
    private Text _upgradeText;

    [SerializeField]
    private GameObject _ui;

    [SerializeField]
    private Button btn;

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
        _level = _transitionSaver.RecoverUpgradeBench(_type);
        DisplayChange();
    }


    public void Upgrade()
    {
        Debug.Log("okay");
        if(_level < _availableUpgrades.Count)
        {
            if(_transitionSaver.GetScrapCount() >= _loadedUpgrade.GetCost())
            {
                _level++;
                _transitionSaver.SetBenchUpgrade(_type);
                DisplayChange();
            }
        }
    }


    private void DisplayChange()
    {
        _loadedUpgrade = _availableUpgrades[_level];
        _mainText.text = _loadedUpgrade.GetDescriptionText();
        _boostText.text = _loadedUpgrade.GetBoost().ToString();
        _upgradeText.text = "Upgrade for " + _loadedUpgrade.GetCost();
        _boostIcon.sprite = _loadedUpgrade.GetIconSprite();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _ui.SetActive(true);
        DisplayChange();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => Upgrade());
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _ui.SetActive(false);
    }
}