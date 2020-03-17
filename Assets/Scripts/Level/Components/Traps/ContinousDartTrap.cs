using System.Collections;
using UnityEngine;

public class ContinousDartTrap : MonoBehaviour, IActivable, IHidable
{
    [SerializeField]
    private GameObject _dart;

    [SerializeField]
    private float _activatedTime;

    [SerializeField]
    private float _fireRate;

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
        if (_canBeActivated && !_isActive)
        {
            _isActive = true;
            StartCoroutine(ResetState());
        }
    }

    public void Desactivate()
    {
        if (_canBeDesactivated)
        {
            StartCoroutine(ResetActiveState());
            _isActive = false;
        }
    }


    private IEnumerator ShootDarts()
    {
        while(_isActive)
        {
            Projectile newDart = Instantiate(_dart, transform.position, Quaternion.identity).GetComponent<Projectile>();
            newDart.SetDirections(_shootingDirections);
            yield return new WaitForSeconds(_fireRate);
        }
    }


    private IEnumerator ResetState()
    {
        StartCoroutine(ShootDarts());
        yield return new WaitForSeconds(_activatedTime);
        _isActive = false;
    }


    public IEnumerator ResetActiveState()
    {
        yield return new WaitForSeconds(0.5f);
        _isActive = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true); 
        
        if (_canBeActivated && !_isActive)
        {
            _isActive = true;
            StartCoroutine(ResetState());
        }
    }
}