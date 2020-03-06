using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour, ILockable
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _lockedSprite;
    [SerializeField]
    private Sprite _openendSprite;

    private bool _isLocked;

    [SerializeField]
    private List<Lock> _locks;
    public List<Lock> Locks { get => _locks; set => _locks = value; }



    private void Start()
    {
        Lock();
    }


    public void Lock()
    {
        _isLocked = true;
        _spriteRenderer.sprite = _lockedSprite;
    }


    public void Open()
    {
        _isLocked = false;
        _spriteRenderer.sprite = _openendSprite;
    }


    public void OpenOneLock(Lock other)
    {
        if (_locks.Contains(other))
            _locks.Remove(other);

        if (_locks.Count == 0)
            Open();
    }
}