using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private string _zone;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            Debug.Log("hello?");
            Destroy(gameObject);
            switch(_zone)
            {
                case "Forest":
                    FindObjectOfType<TransitionSaver>().SetForestKey(true);
                    break;

                case "Cavern":
                    FindObjectOfType<TransitionSaver>().SetCavernKey(true);
                    break;

                case "Graveyard":
                    FindObjectOfType<TransitionSaver>().SetGraveyardKey(true);
                    break;
            }
        }
    }
}