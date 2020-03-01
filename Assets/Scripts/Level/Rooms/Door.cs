using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite _wallSprite;


    [Header("Components")]
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _collider;


    private bool _canBeOpened = true;



    //-------------------------------Open and Close Methods
    public void RemplaceByWall()
    {
        _spriteRenderer.sprite = _wallSprite;
        _collider.isTrigger = false;
        _canBeOpened = false;
    }


    //-------------------------------Getters
    public bool GetCanBeOpened() { return _canBeOpened; }
}