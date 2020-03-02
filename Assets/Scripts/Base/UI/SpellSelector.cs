using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    [SerializeField]
    private int _spellId;
    private int _actualSpellId = 0;
    [SerializeField]
    private SpellInterfaceController _spellInterfaceController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
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
