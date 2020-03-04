using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;

    [SerializeField]
    private PlayerMovement _player;



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
        SceneManager.LoadScene("Dungeon");
    }


    public void LoadBase()
    {
        SceneManager.LoadScene("Base");
    }

    public int GetScrapCount() { return _scrapCount; }

    public void AddScrapCount (int other) { _scrapCount += other; }

    public void RemoveScrapCount (int other) { _scrapCount -= other; }

    public void SetScrapCount (int other) { _scrapCount = other; }

    public PlayerMovement GetPlayer() { return _player; }
}