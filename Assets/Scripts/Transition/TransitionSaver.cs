using UnityEngine;

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


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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