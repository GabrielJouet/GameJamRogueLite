using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    private int _scrapCount;
    private int _firecampLevel;
    private int _storageLevel;
    private int _wellLevel;
    private int _armorLevel;
    private int _bootsLevel;
    private int _1stSelectedSpell;
    private int _2ndSelectedSpell;
    private int _spellLevel;


    [Header("Upgrades")]
    [SerializeField]
    private List<Upgrade> _fireCampUpgrades;
    [SerializeField]
    private List<Upgrade> _wellUpgrades;
    [SerializeField]
    private List<Upgrade> _spellsUpgrades;
    [SerializeField]
    private List<Upgrade> _shoesUpgrades;
    [SerializeField]
    private List<Upgrade> _armorUpgrades;
    [SerializeField]
    private List<Upgrade> _backpackUpgrades;


    [Header("Player prefabs")]
    [SerializeField]
    private PlayerMovement _player;

    private bool _dungeonLoaded = false;
    private bool _canReturnToBase = true;

    private bool _forestKeyGained = false;
    private bool _cavernKeyGained = false;
    private bool _graveyardKeyGained = false;


    private void Awake()
    {
        if (FindObjectsOfType<TransitionSaver>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
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


    public int RecoverUpgradeBench(string type)
    {
        switch (type)
        {
            case "Fire":
                return _firecampLevel;
                
            case "Shoes":
                return _bootsLevel;

            case "Storage":
                return _storageLevel;

            case "Spells":
                return _spellLevel;

            case "Armor":
                return _armorLevel;

            case "Well":
                return _wellLevel;
            default:
                return 0;
        };
    }

    public void SetBenchUpgrade(string type)
    {
        switch(type)
        {
            case "Fire":
                _firecampLevel++;
                break;
            case "Shoes":
                _bootsLevel++;
                break;
            case "Storage":
                _storageLevel++;
                break;
            case "Spells":
                _spellLevel++;
                break;
            case "Armor":
                _armorLevel++;
                break;
            case "Well":
                _wellLevel ++;
                break;
        };
    }


    public void ApplyPlayerStat(PlayerMovement newPlayer)
    {
        newPlayer.SetMaxHealth(_fireCampUpgrades[_firecampLevel].GetBoost());
        newPlayer.SetMaxStamina(_wellUpgrades[_wellLevel].GetBoost());
        newPlayer.SetStorageMalus(_backpackUpgrades[_storageLevel].GetBoost());
        newPlayer.SetSpeed(_shoesUpgrades[_bootsLevel].GetBoost());
        newPlayer.SetArmor(_armorUpgrades[_armorLevel].GetBoost());
        //newPlayer.SetSupportBoost(_spellsUpgrades[_spellLevel].GetBoost());
    }


    public int GetScrapCount() { return _scrapCount; }

    public void AddScrapCount (int other) { _scrapCount += other; }

    public void RemoveScrapCount (int other) { _scrapCount -= other; }

    public void SetScrapCount (int other) { _scrapCount = other; }
    
    public void Set1stSelectedSpell(int lvl) { _1stSelectedSpell = lvl; }

    public void Set2ndSelectedSpell(int lvl) { _2ndSelectedSpell = lvl; }

    public bool GetCanTeleport() { return _canReturnToBase; }

    public void SetFireCampLvl(int lvl) { _firecampLevel = lvl; }

    public void SetBackpackLvl(int lvl) { _storageLevel = lvl; }

    public void SetWellLvl(int lvl) { _wellLevel = lvl; }

    public void SetArmorLvl(int lvl) { _armorLevel = lvl; }

    public void SetShoesLvl(int lvl) { _bootsLevel = lvl; }

    public void SetSpellLvl(int lvl) { _spellLevel = lvl; }

    public void SetCanTeleport(bool other) { _canReturnToBase = other; }

    public bool GetGraveyardKey() { return _graveyardKeyGained; }

    public bool GetForestKey() { return _forestKeyGained; }

    public bool GetCavernKey() { return _cavernKeyGained; }

    public void SetGraveyardKey(bool other) { _graveyardKeyGained = other; }

    public void SetForestKey(bool other) { _forestKeyGained = other; }

    public void SetCavernKey(bool other) { _cavernKeyGained = other; }

    public PlayerMovement GetPlayer() { return _player; }
}