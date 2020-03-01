using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("Room Counts")]
    [SerializeField]
    [Range(2,55)]
    private int _roomCount;


    [Header("Room Prefabs")]
    [SerializeField]
    private List<Room> _baseRooms = new List<Room>();
    [SerializeField]
    private List<Room> _bodyRooms = new List<Room>();



    //-------------------------------Getters
    public Room GetBaseRoom() { return _baseRooms[Random.Range(0, _baseRooms.Count)]; }

    public Room GetBodyRoom() { return _bodyRooms[Random.Range(0, _bodyRooms.Count)]; }

    public int GetRoomCount() { return _roomCount; }
}