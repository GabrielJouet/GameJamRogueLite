using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellInterfaceController : MonoBehaviour
{
    private int _wellLvl;

    private int _fireCampLvl = 0;
    private int _upgradeCost = 20;

    [SerializeField]
    private Text _MainText;
    [SerializeField]
    private Text _staminaBoostText;
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
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0)) //still have to check if the player have enought scraps
        {
            UpgradeWell();
        }
    }
    private void UpgradeWell()
    {
        if (_wellLvl == 0)
        {
            _wellLvl++;
            _transitionSaver.SetWellLvl(_wellLvl); //still have to check if max lvl is not yet reached.
            _upgradeCost *= 2;
            _MainText.text = "You installed a rustic mechanism\n to access the water, making\n you fill a bit more energic";
            _staminaBoostText.text = "10 %";
            _upgradeText.text = "Upgrade for " + _upgradeCost;
        }
    }
}
