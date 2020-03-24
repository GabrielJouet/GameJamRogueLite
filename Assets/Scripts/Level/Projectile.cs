using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10)]
    private float _speed;

    [SerializeField]
    private int _damage;

    [SerializeField]
    [Range(0f, 100f)]
    private float _dispersion;

    [SerializeField]
    private Vector2 _directions;

    private ProjectilePool _pool;

    private Vector2 _startingPosition;

    private bool _isMoving = false;


    public void Initialize(Vector2 newPosition, Quaternion newAngle)
    {
        _isMoving = true;
        transform.position = newPosition;
        transform.rotation = newAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0,0, transform.localEulerAngles.z + Random.Range(-_dispersion, _dispersion)));

        if(_pool == null)
            _pool = FindObjectOfType<ProjectilePool>();

        _startingPosition = transform.position;
    }


    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_directions * Time.deltaTime * _speed);

            if (((Vector2)transform.position - _startingPosition).magnitude > 4)
                _pool.RetrieveProjectile(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            _pool.RetrieveProjectile(gameObject);
            _isMoving = false;

            collision.GetComponent<PlayerMovement>().TakeDamage(_damage);
        }
    }
}