using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;
    [SerializeField]
    private int _firecampLvl;
    private int _storageLvl;
    private int _wellLvl;
    private int _armorLvl;
    private int _bootsLvl;
    private int _selectedSpell;
    private int _spellLvl;
    


    [SerializeField]
    private PlayerMovement _player;

    
    private bool _dungeonLoaded = false;


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

    public void SetStorageLvl(int lvl) { _storageLvl = lvl; }

    public void SetWellLvl(int lvl) { _wellLvl = lvl; }

    public void SetArmorLvl(int lvl) { _armorLvl = lvl; }

    public void SetBootsLvl(int lvl) { _bootsLvl = lvl; }

    public void SetSelectedSpell(int lvl) { _selectedSpell = lvl; }

    public void SetSpellLvl(int lvl) { _spellLvl = lvl; }



    public PlayerMovement GetPlayer() { return _player; }
}