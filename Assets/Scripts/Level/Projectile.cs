using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private int _damage;

    private ProjectilePool _pool;
    private Vector2 _startingPosition;

    private bool _isMoving = false;


    public void Initialize(Vector2 newPosition, Quaternion newAngle, int newDamage, float newDispersion, float newSpeed)
    {
        _isMoving = true;
        transform.position = newPosition;
        transform.rotation = newAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0,0, transform.localEulerAngles.z + Random.Range(-newDispersion, newDispersion)));

        _speed = newSpeed;
        _damage = newDamage;

        if(_pool == null)
            _pool = FindObjectOfType<ProjectilePool>();

        _startingPosition = transform.position;
    }


    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(new Vector2(0,-1) * Time.deltaTime * _speed);

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