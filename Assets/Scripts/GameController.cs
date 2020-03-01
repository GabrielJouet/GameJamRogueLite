using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private CreationLevel _creationLevel;



    //-------------------------------Unity Methods
    private void Start()
    {
        int rng = System.Environment.TickCount;
        Random.InitState(rng);
        Debug.Log(rng);

        //When we launch the game we create a new level
        _creationLevel.CreateLevel();
    }
}