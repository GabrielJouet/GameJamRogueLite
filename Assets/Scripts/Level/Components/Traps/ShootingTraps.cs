using UnityEngine;

public class ShootingTraps : MonoBehaviour
{
    [Header("Shot Parameters")]
    [SerializeField]
    protected GameObject _projectile;
    [SerializeField]
    protected float _activatedTime;
    [SerializeField]
    protected float _fireRate;
    [SerializeField]
    protected Vector2 _shootingDirections;
    [SerializeField]
    protected Transform _shootingStartPoint;

    protected ProjectilePool _pool;


    protected void Awake()
    {
        _pool = FindObjectOfType<ProjectilePool>();
    }
}