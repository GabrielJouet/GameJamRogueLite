using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _menuElements;
    private int _index = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (_index == 0)
                _index = _menuElements.Count - 1;
            else
                _index--;

            ActivateMenuElements();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_index == _menuElements.Count - 1)
                _index = 0;
            else
                _index++;

            ActivateMenuElements();
        }

        Debug.Log(_index);

        if (Input.GetKeyDown(KeyCode.Return))
            _menuElements[_index].GetComponent<IChoosable>().Choose();
    }


    private void ActivateMenuElements()
    {
        for(int i = 0; i < _menuElements.Count; i ++)
        {
            if(i == _index)
                _menuElements[i].GetComponent<IChoosable>().Activate();
            else
                _menuElements[i].GetComponent<IChoosable>().Desactivate();
        }
    }
}