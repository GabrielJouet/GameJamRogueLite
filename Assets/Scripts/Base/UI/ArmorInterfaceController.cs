using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorInterfaceController : MonoBehaviour
{
    private int _armorLvl;
    private int _upgradeCost = 20;

    [SerializeField]
    private Text _MainText;
    [SerializeField]
    private Text _armorBoostText;
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
    public void UpgradeArmor()
    {
        if (_armorLvl == 0)
        {
            _armorLvl++;
            _transitionSaver.SetWellLvl(_armorLvl); //still have to check if max lvl is not yet reached.
            _upgradeCost *= 2;
            _MainText.text = "Your armor is now poorly fixed, making you a bit more resistant to any damage";
            _armorBoostText.text = "10";
            _upgradeText.text = "Upgrade for " + _upgradeCost;
        }
    }
}
