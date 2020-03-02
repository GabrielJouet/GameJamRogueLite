using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room Counts")]
    [SerializeField]
    [Range(2,55)]
    private int _roomCount;
    
    [SerializeField]
    [Range(0, 5)]
    private int _secretRoomCount;


    [Header("Room Prefabs")]
    [SerializeField]
    private List<Room> _baseRooms = new List<Room>();
    [SerializeField]
    private List<Room> _bodyRooms = new List<Room>();
    [SerializeField]
    private List<Room> _bossRooms = new List<Room>();
    [SerializeField]
    private List<Room> _itemRooms = new List<Room>();
    [SerializeField]
    private List<Room> _secretRooms = new List<Room>();



    //-------------------------------Getters
    public Room GetBaseRoom() { return _baseRooms[Random.Range(0, _baseRooms.Count)]; }

    public Room GetBossRoom() { return _bossRooms[Random.Range(0, _bossRooms.Count)]; }

    public Room GetItemRoom() { return _itemRooms[Random.Range(0, _itemRooms.Count)]; }

    public Room GetBodyRoom() { return _bodyRooms[Random.Range(0, _bodyRooms.Count)]; }

    public Room GetSecretRoom() { return _secretRooms[Random.Range(0, _secretRooms.Count)]; }

    public int GetRoomCount() { return _roomCount; }

    public int GetSecretRoomCount() { return _secretRoomCount; }
}