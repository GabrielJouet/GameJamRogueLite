using UnityEngine;

public class BossRoom : Room
{
    [SerializeField]
    private GameObject _key;


    public new void EnemyKilled(GameObject other)
    {
        _roomEnemies.Remove(other);

        if (_roomEnemies.Count == 0)
        {
            _key.SetActive(true);
            //RoomFinished();
        }
    }
}