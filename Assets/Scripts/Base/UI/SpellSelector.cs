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
            _spellInterfaceController.ChangeSelectedSpell(_spellId);
        }
    }
}
