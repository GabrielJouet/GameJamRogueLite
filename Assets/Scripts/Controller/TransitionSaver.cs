using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;
    [SerializeField]
    private int _firecampLevel;
    [SerializeField]
    private int _storageLevel;
    [SerializeField]
    private int _wellLevel;
    [SerializeField]
    private int _armorLevel;
    [SerializeField]
    private int _bootsLevel;
    [SerializeField]
    private int _1stSelectedSpell;
    [SerializeField]
    private int _2ndSelectedSpell;
    [SerializeField]
    private int _1stSpellLevel;
    [SerializeField]
    private int _2ndSpellLevel;

    [SerializeField]
    private PlayerMovement _player;

    private bool _dungeonLoaded = false;
    private bool _canReturnToBase = true;

    [SerializeField]
    private bool _forestKeyGained = false;
    [SerializeField]
    private bool _cavernKeyGained = false;
    [SerializeField]
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
        Debug.Log(rng);
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
                return _1stSelectedSpell;

            case "Armor":
                return _armorLevel;
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
                _1stSpellLevel++;
                break;
            case "Armor":
                _armorLevel++;
                break;
        };
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

    public void Set1stSpellLvl(int lvl) { _1stSpellLevel = lvl; }

    public void Set2ndSpellLvl(int lvl) { _2ndSpellLevel = lvl; }

    public void SetCanTeleport(bool other) { _canReturnToBase = other; }

    public bool GetGraveyardKey() { return _graveyardKeyGained; }

    public bool GetForestKey() { return _forestKeyGained; }

    public bool GetCavernKey() { return _cavernKeyGained; }

    public void SetGraveyardKey(bool other) { _graveyardKeyGained = other; }

    public void SetForestKey(bool other) { _forestKeyGained = other; }

    public void SetCavernKey(bool other) { _cavernKeyGained = other; }

    public PlayerMovement GetPlayer() { return _player; }
}