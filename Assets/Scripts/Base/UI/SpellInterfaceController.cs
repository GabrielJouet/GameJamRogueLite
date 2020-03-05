using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterfaceController : MonoBehaviour
{

    private int _1stSpellId = 10;
    private int _2ndSpellId;
    private int _maxEquipedSpell = 2;
    private int _nbEquipedSpell = 0;

    [SerializeField]
    private GameObject[] _selector;

    private void Update()
    {
        
    }

    public void ChangeSelectedSpell(int spellId)
    {
        if (_nbEquipedSpell < _maxEquipedSpell && _1stSpellId == 10 && _selector[spellId].activeSelf != true)
        {
            _selector[spellId].SetActive(true);
            _nbEquipedSpell++;
            _1stSpellId = spellId;
            Debug.Log(_nbEquipedSpell + " 1");
        }
        else if (_nbEquipedSpell < _maxEquipedSpell && _1stSpellId != 10 && _selector[spellId].activeSelf != true)
        {
            _selector[spellId].SetActive(true);
            _nbEquipedSpell++;
            _2ndSpellId = spellId;
            Debug.Log(_nbEquipedSpell + " 2");
        }
        else if (_selector[spellId].activeSelf == true)
        {
            _selector[spellId].SetActive(false);
            if (_nbEquipedSpell == 2)
            {
                _2ndSpellId = 10;
                _nbEquipedSpell--;
                Debug.Log(_nbEquipedSpell + " Ok");
            }
            else if (_nbEquipedSpell == 1)
            {
                _nbEquipedSpell--;
                _1stSpellId = 10;
                Debug.Log(_nbEquipedSpell + " Okok");
            }
        }
    }
}
