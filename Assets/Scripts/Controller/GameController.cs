using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CreationLevel _creationLevel;


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

        Debug.Log(_transitionSaver.GetScrapCount());
        _transitionSaver.SetScrapCount(50);

        int rng = System.Environment.TickCount;
        Random.InitState(rng);
        Debug.Log(rng);

        //When we launch the game we create a new level
        _creationLevel.CreateLevel();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadScene("SampleScene");
    }
}