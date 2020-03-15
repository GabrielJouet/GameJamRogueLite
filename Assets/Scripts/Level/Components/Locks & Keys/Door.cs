using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite _closedSprite;
    [SerializeField]
    private Sprite _openedSprite;
    [SerializeField]
    private Sprite _wallSprite;


    [Header("Parent Room")]
    [SerializeField]
    private Room _parentRoom;


    [Header("Components")]
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _collider;

    
    private bool _up;
    private bool _down;
    private bool _left;
    private bool _right;


    private bool _canBeClosed = true;
    private bool _canBeOpened = true;



    private void Start()
    {
        _up = transform.position.y > _parentRoom.transform.position.y;
        _down = transform.position.y < _parentRoom.transform.position.y;
        _left = transform.position.x < _parentRoom.transform.position.x;
        _right = transform.position.x > _parentRoom.transform.position.x;
    }


    //-------------------------------Open and Close Methods
    public void RemplaceByWall()
    {
        _spriteRenderer.sprite = _wallSprite;
        _collider.isTrigger = false;
        _canBeClosed = false;
        _canBeOpened = false;
    }

    
    public void Close()
    {
        _collider.isTrigger = false;
        _spriteRenderer.sprite = _closedSprite;
    }

    
    public void Open()
    {
        _collider.isTrigger = true;
        _spriteRenderer.sprite = _openedSprite;
    }



    //-------------------------------Player Interaction Methods
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            if(_up    && collision.transform.position.y < transform.position.y || 
               _down  && collision.transform.position.y > transform.position.y || 
               _right && collision.transform.position.x < transform.position.x || 
               _left  && collision.transform.position.x > transform.position.x)
            {
                FindObjectOfType<LevelGrid>().MoveMiniMap(_parentRoom);
                FindObjectOfType<FollowCamera>().FollowPlayer(_parentRoom.GetCameraPosition().position);
                _parentRoom.RoomEntered();
            }
        }
    }



    //-------------------------------Getters
    public bool GetCanBeOpened() { return _canBeOpened; }

    public bool GetCanBeClosed() { return _canBeClosed; }
}