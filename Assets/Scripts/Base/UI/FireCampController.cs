using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCampController : MonoBehaviour
{
    private int _fireCampLvl = 0;
    private int _upgradeCost = 20;

    [SerializeField]
    private Text _MainText;
    [SerializeField]
    private Text _vitalityBoostText;
    [SerializeField]
    private Text _upgradeText;

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) //still have to check if the player have enought scraps
        {
            UpgradeFireCamp();
        }
    }

    private void UpgradeFireCamp()
    {
        if (_fireCampLvl == 0)
        {
            _fireCampLvl++; //still have to check if max lvl is not yet reached.
            _upgradeCost *= 2;
            _MainText.text = "The fireplace finally breathe\n again, making you feel\n a bit warmer";
            _vitalityBoostText.text = "10";
            _upgradeText.text = "Upgrade for " + _upgradeCost;
        }
    }
}
