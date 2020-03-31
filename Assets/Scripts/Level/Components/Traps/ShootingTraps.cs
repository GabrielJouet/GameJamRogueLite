using System.Collections.Generic;
using UnityEngine;

public class ShootingTraps : MonoBehaviour
{
    [Header("Shot Parameters")]
    [SerializeField]
    protected GameObject _projectile;
    [SerializeField]
    protected float _fireRate;
    [SerializeField]
    [Range(-1, 1)]
    protected float _xShootingDirection;
    [SerializeField]
    [Range(-1, 1)]
    protected float _yShootingDirection;
    [SerializeField]
    protected Transform _shootingStartPoint;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    [Range(0f, 100f)]
    protected float _dispersion;
    [SerializeField]
    [Range(0.1f, 10)]
    protected float _speed;


    [Header("Sounds-related")]
    [SerializeField]
    protected AudioSource _audioSource;
    [SerializeField]
    protected List<AudioClip> _shotSounds;


    protected ProjectilePool _pool;


    protected void Awake()
    {
        _pool = FindObjectOfType<ProjectilePool>();
    }
}