using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Camera Parameters")]
    [SerializeField]
    private float _cameraSpeed;
    
    private Vector3 _goal;


    private void Start()
    {
        _goal = transform.position;
    }


    private void FixedUpdate()
    {
        //If we are not at the goal position
        if(transform.position != _goal)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_goal.x, _goal.y, -10), Time.fixedDeltaTime * _cameraSpeed);
    }


    
    public void FollowPlayer(Vector3 newGoal)
    {
        _goal = newGoal;
    }
}