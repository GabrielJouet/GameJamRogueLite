using UnityEngine;

public class TransitionSaver : MonoBehaviour
{
    [SerializeField]
    private int _scrapCount;

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

    public PlayerMovement GetPlayer() { return _player; }
}