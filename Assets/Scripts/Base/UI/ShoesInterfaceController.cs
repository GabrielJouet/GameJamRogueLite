using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoesInterfaceController : MonoBehaviour
{
    private int _shoesLvl;
    private int _upgradeCost = 20;

    [SerializeField]
    private Text _MainText;
    [SerializeField]
    private Text _shoesBoostText;
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
    public void UpgradeShoes()
    {
        if (_shoesLvl == 0)
        {
            _shoesLvl++;
            _transitionSaver.SetShoesLvl(_shoesLvl); //still have to check if max lvl is not yet reached.
            _upgradeCost *= 2;
            _MainText.text = "Your shoes are now poorly fixed, making you a bit more at ease";
            _shoesBoostText.text = "5 %";
            _upgradeText.text = "Upgrade for " + _upgradeCost;
        }
    }
}
