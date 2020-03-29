using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateRooms")]
public class Rooms : ScriptableObject
{
    [Header("Room Counts")]
    [SerializeField]
    [Range(4,250)]
    private int _roomCount;
    
    [SerializeField]
    [Range(0, 0)]
    private int _secretRoomCount;


    [Header("Room Prefabs")]
    [SerializeField]
    private List<Room> _baseRooms = new List<Room>();
    [SerializeField]
    private List<Room> _bodyRooms = new List<Room>();
    [SerializeField]
    private List<Room> _bossRooms = new List<Room>();



    //-------------------------------Getters
    public Room GetBaseRoom() { return _baseRooms[Random.Range(0, _baseRooms.Count)]; }

    public Room GetBossRoom() { return _bossRooms[Random.Range(0, _bossRooms.Count)]; }

    public Room GetBodyRoom() { return _bodyRooms[Random.Range(0, _bodyRooms.Count)]; }

    public int GetRoomCount() { return _roomCount; }

    public int GetSecretRoomCount() { return _secretRoomCount; }

    public bool FindRoomContains(Room other)
    {
        return _baseRooms.Contains(other) ||
                _bodyRooms.Contains(other) ||
                _bossRooms.Contains(other);
    }
}