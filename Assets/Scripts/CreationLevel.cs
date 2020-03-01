using System.Collections.Generic;
using UnityEngine;

public class CreationLevel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private LevelGrid _grid;
    [SerializeField]
    private RoomController _roomController;

    
    private List<Room> _levelCreated = new List<Room>();
    private List<Vector2> _availablePlaces = new List<Vector2>();


    //-------------------------------Creation Base Level Methods
    public void CreateLevel()
    {
        _grid.CreateGrid();

        int roomCount = _roomController.GetRoomCount() + Mathf.RoundToInt(Random.Range(0, _roomController.GetRoomCount() * 0.25f));
        
        InstantiateBaseRoom();
        
        for (int i = 0; i < roomCount; i++)
            InstantiateBodyRoom();
            
        PostProcessingRooms();
    }

    
    private void InstantiateBaseRoom()
    {
        Room buffer = Instantiate(_roomController.GetBaseRoom());

        //We put it at 7;7 (the origin)
        buffer.SetX(_grid.GetXSize() / 2);
        buffer.SetY(_grid.GetYSize() / 2);

        //We add this room to the grid, to available room and level created
        _grid.AddRoomToLevel(buffer);

        _levelCreated.Add(buffer);
        _availablePlaces.AddRange(buffer.GetAllNeighbourPlaces());
    }

    
    private void InstantiateBodyRoom()
    {
        Room chosenRoom = ChooseRoom();
        Room bufferRoom = Instantiate(chosenRoom);

        _availablePlaces.Remove(new Vector2(chosenRoom.GetX(), chosenRoom.GetY()));
        bufferRoom.SetX(chosenRoom.GetX());
        bufferRoom.SetY(chosenRoom.GetY());

        _grid.AddRoomToLevel(bufferRoom);

        //We add it to level created and available rooms lists
        _levelCreated.Add(bufferRoom);

        foreach(Vector2 current in bufferRoom.GetAllNeighbourPlaces())
        {
            if (!_availablePlaces.Contains(current))
                _availablePlaces.Add(current);
        }
    }



    //-------------------------------Check Place Methods
    private Room ChooseRoom()
    {
        Room buffer;
        
        //While the layout of the room is not correct
        do
        {
            buffer = _roomController.GetBodyRoom();
            Vector2 bufferedPlace = FindOneAvailablePlace();

            //We set x and y positions
            buffer.SetX(Mathf.FloorToInt(bufferedPlace.x));
            buffer.SetY(Mathf.FloorToInt(bufferedPlace.y));
        }
        while (!_grid.FindRoomAvailability(buffer));

        return buffer;
    }


    private Vector2 FindOneAvailablePlace()
    {
        Vector2 buffer;
        bool result;

        do
        {
            buffer = _availablePlaces[Random.Range(0, _availablePlaces.Count)];

            result = _grid.FindPlaceAvailability(Mathf.FloorToInt(buffer.x), Mathf.FloorToInt(buffer.y));

            if (!result)
                _availablePlaces.Remove(buffer);
        }
        while (!result);

        return buffer;
    }



    //-------------------------------Post Process Level Methods
    private void PostProcessingRooms()
    {
        foreach (Room current in _levelCreated)
            current.InitializeRoom();
    }



    //-------------------------------Getter
    public Vector3 GetSpawnPoint() { return _levelCreated[0].transform.position; }
}