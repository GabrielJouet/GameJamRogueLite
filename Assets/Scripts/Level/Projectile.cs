using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10)]
    private float _speed;

    [SerializeField]
    [Range(0f,100f)]
    private float _dispersion;


    private Vector2 _directions;



    private void Start()
    {
        transform.Rotate( new Vector3(0,0, Random.Range(-_dispersion, _dispersion)));
    }


    private void Update()
    {
        transform.Translate(_directions * Time.deltaTime * _speed);
    }


    public void SetDirections(Vector2 newDirections) { _directions = newDirections; }
}