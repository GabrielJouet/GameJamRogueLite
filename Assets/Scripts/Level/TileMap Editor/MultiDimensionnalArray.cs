using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MultiDimensionnalArray
{
    [SerializeField]
    private List<int> _values = new List<int>();


    //-------------------------------Getter & Setter
    public List<int> GetValues() { return _values; }

    public void AddValue(int value) { _values.Add(value); }
}