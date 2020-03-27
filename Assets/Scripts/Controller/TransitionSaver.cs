using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField]
    private List<Upgrade> _fireCampUpgrades;
    [SerializeField]
    private List<Upgrade> _shoesUpgrades;
    [SerializeField]
    private List<Upgrade> _armorUpgrades;
    [SerializeField]
    private List<Upgrade> _backpackUpgrades;


    [Header("Player prefabs")]
    [SerializeField]
    private PlayerMovement _player;


    private bool _forestKeyGained = false;
    private bool _cavernKeyGained = false;
    private bool _graveyardKeyGained = false;

    private bool _dungeonLoaded = false;

    private int _scrapCount;
    private int _firecampLevel;
    private int _backpackLevel;
    private int _armorLevel;
    private int _shoesLevel;


    private void Awake()
    {
        if (FindObjectsOfType<TransitionSaver>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        int rng = System.Environment.TickCount;
        Random.InitState(rng);
    }



    public void LoadDungeon()
    {
        if(!_dungeonLoaded)
        {
            SceneManager.LoadScene("Dungeon");
            _dungeonLoaded = true;
        }
    }


    public void LoadBase()
    {
        if(_dungeonLoaded)
        {
            SceneManager.LoadScene("Base");
            _dungeonLoaded = false;
        }
    }


    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }


    public Upgrade RecoverUpgrade(string type)
    {
        switch (type)
        {
            case "Fire":
                return _firecampLevel < _fireCampUpgrades.Count ? _fireCampUpgrades[_firecampLevel] : null;
                
            case "Shoes":
                return _shoesLevel < _shoesUpgrades.Count ? _shoesUpgrades[_shoesLevel] : null;

            case "Storage":
                return _backpackLevel < _backpackUpgrades.Count ? _backpackUpgrades[_backpackLevel] : null;

            case "Armor":
                return _armorLevel < _armorUpgrades.Count ? _armorUpgrades[_armorLevel] : null;

            default:
                return null;
        };
    }


    public void UpgradeBench(string type)
    {
        switch(type)
        {
            case "Fire":
                if(_firecampLevel + 1 < _fireCampUpgrades.Count)
                    _firecampLevel++;
                FindObjectOfType<PlayerMovement>().SetMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
                FindObjectOfType<PlayerMovement>().SetHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
                break;

            case "Shoes":
                if (_shoesLevel + 1 < _shoesUpgrades.Count)
                    _shoesLevel++;
                break;

            case "Storage":
                if (_backpackLevel + 1 < _backpackUpgrades.Count)
                    _backpackLevel++;
                break;

            case "Armor":
                if (_armorLevel + 1 < _armorUpgrades.Count)
                    _armorLevel++;
                break;
        };
    }


    public void SetPlayerStats(PlayerMovement other, bool home)
    {
        other.SetMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        other.SetHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        other.SetArmor(_armorUpgrades[_armorLevel].GetBoost());
        other.SetSpeed(_shoesUpgrades[_shoesLevel].GetBoost());
        other.SetStorageMalus(_backpackUpgrades[_backpackLevel].GetBoost());

        if (home)
            other.SetScrap(_scrapCount);
    }


    public void RemoveScrap(int scrapCount) 
    {
        _scrapCount -= scrapCount;
        FindObjectOfType<PlayerMovement>().SetScrap(_scrapCount);

    }

    public void AddScrapCount (int scrapCount) 
    {
        _scrapCount += scrapCount;
        FindObjectOfType<PlayerMovement>().SetScrap(_scrapCount);
    }

    public int GetScrapCount () { return _scrapCount; }

    public bool GetGraveyardKey () { return _graveyardKeyGained; }

    public bool GetForestKey () { return _forestKeyGained; }

    public bool GetCavernKey () { return _cavernKeyGained; }

    public void SetGraveyardKey (bool other) { _graveyardKeyGained = other; }

    public void SetForestKey (bool other) { _forestKeyGained = other; }

    public void SetCavernKey (bool other) { _cavernKeyGained = other; }

    public PlayerMovement GetPlayer () { return _player; }
}