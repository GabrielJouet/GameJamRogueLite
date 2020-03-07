using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;
    [SerializeField]
    private int _firecampLvl;
    [SerializeField]
    private int _storageLvl;
    [SerializeField]
    private int _wellLvl;
    [SerializeField]
    private int _armorLvl;
    [SerializeField]
    private int _bootsLvl;
    [SerializeField]
    private int _1stSelectedSpell;
    [SerializeField]
    private int _2ndSelectedSpell;
    [SerializeField]
    private int _1stSpellLvl;
    [SerializeField]
    private int _2ndSpellLvl;

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

    public int GetScrapCount() { return _scrapCount; }

    public void AddScrapCount (int other) { _scrapCount += other; }

    public void RemoveScrapCount (int other) { _scrapCount -= other; }

    public void SetScrapCount (int other) { _scrapCount = other; }

    public void SetFireCampLvl(int lvl){ _firecampLvl = lvl; }

    public void SetBackpackLvl(int lvl) { _storageLvl = lvl; }

    public void SetWellLvl(int lvl) { _wellLvl = lvl; }

    public void SetArmorLvl(int lvl) { _armorLvl = lvl; }

    public void SetShoesLvl(int lvl) { _bootsLvl = lvl; }

    public void Set1stSelectedSpell(int lvl) { _1stSelectedSpell = lvl; }

    public void Set2ndSelectedSpell(int lvl) { _2ndSelectedSpell = lvl; }

    public void Set1stSpellLvl(int lvl) { _1stSpellLvl = lvl; }

    public void Set2ndSpellLvl(int lvl) { _2ndSpellLvl = lvl; }

    public bool GetCanTeleport() { return _canReturnToBase; }

    public void SetCanTeleport(bool other) { _canReturnToBase = other; }

    public bool GetGraveyardKey() { return _graveyardKeyGained; }

    public bool GetForestKey() { return _forestKeyGained; }

    public bool GetCavernKey() { return _cavernKeyGained; }

    public void SetGraveyardKey(bool other) { _graveyardKeyGained = other; }

    public void SetForestKey(bool other) { _forestKeyGained = other; }

    public void SetCavernKey(bool other) { _cavernKeyGained = other; }

    public PlayerMovement GetPlayer() { return _player; }
}