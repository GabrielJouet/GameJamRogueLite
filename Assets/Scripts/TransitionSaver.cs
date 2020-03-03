using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public int GetScrapCount() { return _scrapCount; }

    public void AddScrapCount (int other) { _scrapCount += other; }

    public void RemoveScrapCount (int other) { _scrapCount -= other; }

    public void SetScrapCount (int other) { _scrapCount = other; }
}