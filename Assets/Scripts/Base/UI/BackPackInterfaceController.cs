using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackInterfaceController : MonoBehaviour
{
    private int _backpackLvl;
    private int _upgradeCost = 20;

    [SerializeField]
    private Text _MainText;
    [SerializeField]
    private Text _backpackBoostText;
    [SerializeField]
    private Text _upgradeText;

    private TransitionSaver _transitionSaver;

    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        if (_transitionSaver == null)
        {
            Debug.Log("transition Server Object hasn't been instantiated");
        }
    }
    public void UpgradeBackpack()
    {
        if (_backpackLvl == 0)
        {
            _backpackLvl++;
            _transitionSaver.SetBackpackLvl(_backpackLvl); //still have to check if max lvl is not yet reached.
            _upgradeCost *= 2;
            _MainText.text = "Your bag feel more stonger and resistant, making you able to carry more scrap.";
            _backpackBoostText.text = "40";
            _upgradeText.text = "Upgrade for " + _upgradeCost;
        }
    }
}
