using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
            FindObjectOfType<TransitionSaver>().LoadDungeon();
    }
}