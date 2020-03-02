using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterfaceController : MonoBehaviour
{

    private int _actualSpellId = 0;

    [SerializeField]
    private SpellSelector[] spellSelector;

    [SerializeField]
    private GameObject[] selector;

    public void ChangeSelectedSpell(int spellId)
    {
        if (spellId != _actualSpellId)
        {
            selector[spellId].SetActive(true);
            selector[_actualSpellId].SetActive(false);
            _actualSpellId = spellId;
        }
      
    }
}
