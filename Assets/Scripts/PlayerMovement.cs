using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private float _maxHealth;
    private float _health;
    [SerializeField]
    private float _speed = 15000;
    [SerializeField]
    private float _dashSpeed = 90000;
    private float _dashImmunityTime = .2f;
    private float _dashCooldown = 1f;
    private float _maxSpeed;
    [SerializeField]
    private float _armor;
    [SerializeField]
    private float _storageMalus;
    [SerializeField]
    private float _timeBeforeReHit;


    [Header("Audio Files")]
    [SerializeField]
    private AudioClip _collectScrapSound;


    private int _scrapCount = 0;
    private bool _canMove = true;
    private bool _canDash = true;
    private bool _isDashing = false;


    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Animator _animator;


    private TransitionSaver _transitionSaver;
    private PlayerUI _playerUI;

    private bool _canBeHit = true;


    public void Initialize(TransitionSaver newSaver, PlayerUI playerUI, bool inHome)
    {
        _transitionSaver = newSaver;
        _playerUI = playerUI;

        _transitionSaver.SetPlayerStats(this, inHome);
    }


    private void FixedUpdate()
    {
        Vector2 playerInputs = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        Vector2 mainDirection = Mathf.Abs(playerInputs.x) > Mathf.Abs(playerInputs.y) ?
            new Vector2(Mathf.Sign(playerInputs.x), 0) :
            new Vector2(0, Mathf.Sign(playerInputs.y));

        if (_canMove)
            _rigidBody.AddForce( Vector2.ClampMagnitude(playerInputs, 1) * _speed * Time.deltaTime );

        if (Input.GetKeyDown(KeyCode.Space) && _canDash)
            StartCoroutine( Dash(mainDirection) );
    }


    private IEnumerator Dash(Vector2 direction)
    {
        _canDash = false;
        _isDashing = true;

        _rigidBody.AddForce(direction * _dashSpeed * Time.deltaTime);

        yield return new WaitForSeconds(_dashImmunityTime);
        _isDashing = false;

        yield return new WaitForSeconds(_dashCooldown - _dashImmunityTime);
        _canDash = true;
    }


    public void TakeDamage(int damage)
    {
        if(_canBeHit && !_isDashing)
        {
            _canBeHit = false;
            _animator.SetBool("takeDamage", true);

            if (_health - Mathf.FloorToInt(damage - damage * _armor / 100f) <= 0)
            {
                _health = 0;
                _canMove = false;
                StartCoroutine(Die());
            }
            else
                _health -= Mathf.FloorToInt(damage - damage * _armor / 100f);

            _playerUI.SetHealth(_health);

            StartCoroutine(ResetCanBeHit());
        }
    }


    private IEnumerator ResetCanBeHit()
    {
        yield return new WaitForSeconds(_timeBeforeReHit);
        _canBeHit = true;
        _animator.SetBool("takeDamage", false);
    }


    private IEnumerator Die()
    {
        _playerUI.ShowDeathPanel(true, _scrapCount);

        yield return new WaitForSeconds(2.5f);
        _transitionSaver.AddScrapCount(_scrapCount);
        _transitionSaver.LoadBase();
    }


    public void Win()
    {
        StartCoroutine(DisplayWin());
    }


    private IEnumerator DisplayWin()
    {
        _playerUI.ShowDeathPanel(false, _scrapCount);

        yield return new WaitForSeconds(3.5f);
        _transitionSaver.AddScrapCount(_scrapCount);
        _transitionSaver.LoadEnd();
    }


    public void CollectScrap(int value)
    {
        _scrapCount += value;
        float speedMalus = _maxSpeed - (_maxSpeed * (_scrapCount * _storageMalus) / 100f);

        _speed = speedMalus < 0.4f * _maxSpeed ? 0.4f * _maxSpeed : speedMalus;

        _playerUI.SetScrap(_scrapCount);

        _audioSource.clip = _collectScrapSound;
        _audioSource.Play();
    }


    //--------------------------------Setter
    public void SetMaxHealth(float health) 
    { 
        _maxHealth = health;
        _playerUI.SetMaxHealth(_maxHealth);
    }

    public void SetHealth(float health)
    {
        _health = health;
        _playerUI.SetHealth(_maxHealth);
    }

    public void SetStorageMalus(float storage) 
    {
        _storageMalus = storage; 
    }

    public void SetArmor(float armor) 
    { 
        _armor = armor; 
    }

    public void SetSpeed(float speed) 
    { 
        _speed = speed;
        _maxSpeed = _speed;
    }


    public void SetScrap(int scrap) 
    { 
        _scrapCount = scrap;
        _playerUI.SetScrap(_scrapCount);
    }

    public void SetCanMove(bool other) { _canMove = other; }
}