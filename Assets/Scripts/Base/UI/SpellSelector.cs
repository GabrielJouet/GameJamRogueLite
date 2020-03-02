using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    [SerializeField]
    private int _spellId;
    [SerializeField]
    private SpellInterfaceController _spellInterfaceController;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {

            switch (_spellId)
            {
                case 0:
                    _spellInterfaceController.ChangeSelectedSpell(_spellId);
                    break;
                case 1:
                    _spellInterfaceController.ChangeSelectedSpell(_spellId);
                    break;
                case 2:
                    _spellInterfaceController.ChangeSelectedSpell(_spellId);
                    break;
                case 3:
                    _spellInterfaceController.ChangeSelectedSpell(_spellId);
                    break;
            }
        }
    }
}
