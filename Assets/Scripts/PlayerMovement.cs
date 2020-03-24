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
    private float _maxSpeed;
    [SerializeField]
    private float _armor;
    [SerializeField]
    private float _storageMalus;


    [Header("Audio Files")]
    [SerializeField]
    private AudioClip _collectScrapSound;


    private int _scrapCount = 0;
    private bool _canMove = true;


    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private AudioSource _audioSource;


    private TransitionSaver _transitionSaver;
    private PlayerUI _playerUI;


    public void Initialize(TransitionSaver newSaver, PlayerUI playerUI, bool inHome)
    {
        _transitionSaver = newSaver;
        _playerUI = playerUI;

        _transitionSaver.SetPlayerStats(this, inHome);
    }


    private void Update()
    {
        if(_canMove)
            _rigidBody.AddForce(Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1) * _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(2);
    }


    private void TakeDamage(int damage)
    {
        if(_health - damage <= 0)
        {
            _health = 0;
            _canMove = false;
            StartCoroutine(Die());
        }
        else 
            _health -= damage;

        _playerUI.SetHealth(_health);
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

        _speed = speedMalus < 0.5f * _maxSpeed ? 0.5f * _maxSpeed : speedMalus;

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