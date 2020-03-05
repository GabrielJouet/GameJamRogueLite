using UnityEngine;

public class BossRoom : Room
{
    [SerializeField]
    private GameObject _levelTransition;


    public new void EnemyKilled(GameObject other)
    {
        _roomEnemies.Remove(other);

        if (_roomEnemies.Count == 0)
        {
            _levelTransition.SetActive(true);
            //RoomFinished();
        }
    }
}