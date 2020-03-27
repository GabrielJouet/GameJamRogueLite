using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField]
    private string _keyNeeded;

    [SerializeField]
    private Sprite _locked;
    [SerializeField]
    private Sprite _opened;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;


    [SerializeField]
    private GameObject _lockedDoor;


    private bool _isLocked = true;


    private TransitionSaver _saver;



    private void Start()
    {
        _saver = FindObjectOfType<TransitionSaver>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() && _isLocked)
        {
            if(_keyNeeded == "Forest" && _saver.GetForestKey() ||
               _keyNeeded == "Cavern" && _saver.GetCavernKey() ||
               _keyNeeded == "Graveyard" && _saver.GetGraveyardKey())
            {
                _isLocked = false;
                _spriteRenderer.sprite = _opened;
                _lockedDoor.GetComponent<ILockable>().OpenOneLock(this);
            }
        }
    }
}