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
    private PlayerUI _playerUI;


    private bool _forestKeyGained = false;
    private bool _cavernKeyGained = false;
    private bool _graveyardKeyGained = false;

    private bool _dungeonLoaded = false;

    private int _scrapCount;
    private int _firecampLevel;
    private int _storageLevel;
    private int _armorLevel;
    private int _bootsLevel;


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


    public List<Upgrade> RecoverUpgrades(string type)
    {
        switch (type)
        {
            case "Fire":
                return _fireCampUpgrades;
                
            case "Shoes":
                return _shoesUpgrades;

            case "Storage":
                return _backpackUpgrades;

            case "Armor":
                return _armorUpgrades;

            default:
                return null;
        };
    }


    public int RecoverUpgradesNumber(string type)
    {
        switch (type)
        {
            case "Fire":
                return _firecampLevel;

            case "Shoes":
                return _bootsLevel;

            case "Storage":
                return _storageLevel;

            case "Armor":
                return _armorLevel;

            default:
                return 0;
        };
    }


    public void UpgradeBench(string type)
    {
        switch(type)
        {
            case "Fire":
                _firecampLevel++;
                UpdatePlayerMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
                UpdatePlayerHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
                break;
            case "Shoes":
                _bootsLevel++;
                break;
            case "Storage":
                _storageLevel++;
                break;
            case "Armor":
                _armorLevel++;
                break;
        };
    }


    public void SetPlayerStats(PlayerMovement other)
    {
        other.SetMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        other.SetHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        other.SetArmor(_armorUpgrades[_armorLevel].GetBoost());
        other.SetSpeed(_shoesUpgrades[_bootsLevel].GetBoost());
        other.SetStorageMalus(_backpackUpgrades[_storageLevel].GetBoost());

        UpdatePlayerHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        UpdatePlayerMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        UpdateScrapCount(_scrapCount);
    }


    public void UpdatePlayerHealth(float health)
    {
        if (_playerUI == null)
            _playerUI = FindObjectOfType<PlayerUI>();

        _playerUI.SetHealth(health);
    }


    public void UpdatePlayerMaxHealth(float maxHealth)
    {
        if (_playerUI == null)
            _playerUI = FindObjectOfType<PlayerUI>();

        _playerUI.SetMaxHealth(maxHealth);
    }


    public void UpdateScrapCount(int scrapCount)
    {
        if (_playerUI == null)
            _playerUI = FindObjectOfType<PlayerUI>();
        _playerUI.SetScrap(scrapCount);
    }


    public void RemoveScrap(int scrapCount) 
    {
        _scrapCount -= scrapCount;
        UpdateScrapCount(_scrapCount); 
    }

    public void AddScrapCount (int scrapCount) 
    {
        _scrapCount += scrapCount;
        UpdateScrapCount(_scrapCount);
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