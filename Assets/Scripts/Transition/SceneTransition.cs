using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private GameController _controller;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.GetComponent<Player>())
        _controller.LoadDungeon();
    }
}