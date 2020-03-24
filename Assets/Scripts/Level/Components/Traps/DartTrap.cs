using System.Collections;
using UnityEngine;

public class DartTrap : ShootingTraps, IActivable, IHidable
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
        _isActive = true;
        ShotDart();
    }


    private void ShotDart()
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

        if(_isActive)
        {
            Projectile newProjectile = _pool.RecoverProjectile(_projectile).GetComponent<Projectile>();
            newProjectile.Initialize(_shootingStartPoint.position, Quaternion.Euler(new Vector3(0, 0, transform.localEulerAngles.z)));
        }
    }

    public void Desactivate()
    {
        if (!_isAlwaysActive)
        {
            _isActive = false;
            StartCoroutine(ResetActiveState());
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

        if(_isAlwaysActive)
            Activate();
    }
}