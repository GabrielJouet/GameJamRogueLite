using System.Collections;
using UnityEngine;

public class ContinousDartTrap : ShootingTraps, IActivable, IHidable
{
    [Header("Activation Parameters")]
    [SerializeField]
    private bool _isAlwaysActive;
    public bool IsAlwaysActive { get => _isAlwaysActive; set => _isAlwaysActive = value; }
    [SerializeField]
    private bool _isActive;
    public bool IsActive { get => _isActive; set => _isActive = value; }



    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            StartCoroutine(ShootDarts());

            if(!_isAlwaysActive)
                StartCoroutine(ResetState());
        }
    }


    public void Desactivate()
    {
        if (!_isAlwaysActive)
        {
            StartCoroutine(ResetActiveState());
            _isActive = false;
        }
    }


    private IEnumerator ShootDarts()
    {
        float angle = 0f;

        if (_shootingDirections.x == -1)
            angle = -90;
        else if (_shootingDirections.x == 1)
            angle = 90;

        if (_shootingDirections.y == -1)
            angle = 0;
        else if (_shootingDirections.y == 1)
            angle = 180;

        while (_isActive)
        {
            Instantiate(_projectile, _shootingStartPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            yield return new WaitForSeconds(_fireRate);
        }
    }


    private IEnumerator ResetState()
    {
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
        _isActive = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (_isAlwaysActive)
            Activate();
    }
}