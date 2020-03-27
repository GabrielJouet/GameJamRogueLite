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

        if(_xShootingDirection == -1)
        {
            if (_yShootingDirection == -1)
                angle = -45;
            else if (_yShootingDirection == 0)
                angle = -90;
            else if (_yShootingDirection == 1)
                angle = -135;
        }
        else if (_xShootingDirection == 0)
        {
            if (_yShootingDirection == -1)
                angle = 0;
            else if (_yShootingDirection == 0)
                angle = 0;
            else if (_yShootingDirection == 1)
                angle = 180;
        }
        else if (_xShootingDirection == 1)
        {
            if (_yShootingDirection == -1)
                angle = 45;
            else if (_yShootingDirection == 0)
                angle = 90;
            else if (_yShootingDirection == 1)
                angle = -135;
        }

        while (_isActive)
        {
            _audioSource.Stop();
            _audioSource.clip = _shotSounds[Random.Range(0, _shotSounds.Count)];
            _audioSource.Play();
            Projectile newProjectile = _pool.RecoverProjectile(_projectile).GetComponent<Projectile>();
            newProjectile.Initialize(_shootingStartPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));

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