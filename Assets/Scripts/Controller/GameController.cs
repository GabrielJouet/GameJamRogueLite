using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject _transitionSaverPrefab;


    private TransitionSaver _transitionSaver;


    //-------------------------------Unity Methods
    private void Start()
    {
        _transitionSaver = FindObjectOfType<TransitionSaver>();
        if (_transitionSaver == null)
            _transitionSaver = Instantiate(_transitionSaverPrefab).GetComponent<TransitionSaver>();

        int rng = System.Environment.TickCount;
        Random.InitState(rng);
        Debug.Log(rng);
    }



    public void LoadDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }
}