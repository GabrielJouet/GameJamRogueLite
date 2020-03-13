using UnityEngine;

public class DartTrap : MonoBehaviour, IActivable
{
    [SerializeField]
    private GameObject _dart;

    [SerializeField]
    private bool _canBeActivated;
    public bool CanBeActivated { get => _canBeActivated; set => _canBeActivated = value; }

    [SerializeField]
    private bool _canBeDesactivated;
    public bool CanBeDesactivated { get => _canBeDesactivated; set => _canBeDesactivated = value; }

    [SerializeField]
    private bool _isActive;
    public bool IsActive { get => _isActive; set => _isActive = value; }

    [SerializeField]
    private Vector2 _shootingDirections;


    public void Activate()
    {
        if(_canBeActivated)
        {
            Projectile newDart = Instantiate(_dart, transform.position, Quaternion.identity).GetComponent<Projectile>();
            newDart.SetDirections(_shootingDirections);
        }
    }

    public void Desactivate()
    {
        if (_canBeDesactivated)
            _isActive = false;
    }
}