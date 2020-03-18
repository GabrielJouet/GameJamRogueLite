using System.Collections;
using UnityEngine;

public class RotatingTurret : ShootingTraps, IActivable, IHidable
{
    [SerializeField]
    private float _rotatingSpeed;


    [Header("Activation Parameters")]
    [SerializeField]
    private bool _isAlwaysActive;
    public bool IsAlwaysActive { get => _isAlwaysActive; set => _isAlwaysActive = value; }
    [SerializeField]
    private bool _isActive;
    public bool IsActive { get => _isActive; set => _isActive = value; }



    private void Update()
    {
        transform.Rotate(new Vector3(0,0,_rotatingSpeed * Time.deltaTime));
    }


    public void Activate()
    {
        if (!_isActive)
        {
            _isActive = true;
            StartCoroutine(ShootDarts());
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


    private IEnumerator ShootDarts()
    {
        Debug.Log("shoot?");
        while (_isActive)
        {
            Debug.Log("shoot");
            Instantiate(_projectile, _shootingStartPoint.position, Quaternion.Euler(new Vector3(0,0, transform.localEulerAngles.z)));
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
        _isActive = false;
    }


    public void Show()
    {
        gameObject.SetActive(true);

        if(_isAlwaysActive)
            Activate();
    }
}