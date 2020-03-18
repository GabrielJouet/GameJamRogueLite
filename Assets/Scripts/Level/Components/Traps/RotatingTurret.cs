using System.Collections;
using UnityEngine;

public class RotatingTurret : MonoBehaviour, IActivable, IHidable
{
    [SerializeField]
    private GameObject _dart;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _rotatingSpeed;

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


    private void Update()
    {
        transform.Rotate(new Vector3(0,0,_rotatingSpeed * Time.deltaTime));
    }


    public void Activate()
    {
        if (_canBeActivated && !_isActive)
        {
            _isActive = true;
            StartCoroutine(ShootDarts());
        }
    }

    public void Desactivate()
    {
        if (_canBeDesactivated)
        {
            _isActive = false;
            StartCoroutine(ResetActiveState());
        }
    }


    private IEnumerator ShootDarts()
    {
        while (_isActive)
        {
            Projectile newDart = Instantiate(_dart, transform.position, Quaternion.Euler(transform.localEulerAngles)).GetComponent<Projectile>();
            newDart.SetDirections(_shootingDirections);
            yield return new WaitForSeconds(_fireRate);
        }
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
        if (_isActive)
            StartCoroutine(ShootDarts());
    }
}