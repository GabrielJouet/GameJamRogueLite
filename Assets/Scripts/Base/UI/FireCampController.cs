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

    private TransitionSaver _transitionSaver;
    [SerializeField]
    private Animator _campFireAnimator;

    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        if (_transitionSaver == null)
        {
            Debug.Log("transition Server Object hasn't been instantiated");
        }
    }

    public void UpgradeFireCamp()
    {
        switch (_fireCampLvl) //still have to check if player has enough scrap for each lvl
        {
            case 0:
                _fireCampLvl++;
                _transitionSaver.SetFireCampLvl(_fireCampLvl); //still have to check if max lvl is not yet reached.
                _upgradeCost *= 2;
                _MainText.text = "The fireplace finally\nbreathe again, making you feel a bit warmer.";
                _vitalityBoostText.text = "10";
                _upgradeText.text = "Upgrade for " + _upgradeCost;
                _campFireAnimator.SetTrigger("campFireLvl1&2");
                break;
            case 1:
                _fireCampLvl++;
                _transitionSaver.SetFireCampLvl(_fireCampLvl); //still have to check if max lvl is not yet reached.
                _upgradeCost *= 2;
                _MainText.text = "The fireplace is stronger\n making you feel warm.";
                _vitalityBoostText.text = "20";
                _upgradeText.text = "Upgrade for " + _upgradeCost;
                _campFireAnimator.SetTrigger("campFireLvl1&2");
                break;
        }
        
            
        
    }
}
