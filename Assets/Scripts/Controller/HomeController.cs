using UnityEngine;

public class HomeController : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;

    private void Start()
    {
        TransitionSaver saver = FindObjectOfType<TransitionSaver>();

        PlayerMovement newPlayer = Instantiate(saver.GetPlayer(), _spawnPoint.position, Quaternion.identity);
        newPlayer.Initialize(saver);
    }
}