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
        Vector2 buffer = Vector2.zero;
        bool result = false;

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


    private void CheckAllAvailablePlaces()
    {
        List<Vector2> uselessPlaces = new List<Vector2>();

        foreach (Vector2 current in _availablePlaces)
        {
            if (!_grid.FindPlaceAvailability(Mathf.FloorToInt(current.x), Mathf.FloorToInt(current.y)))
                uselessPlaces.Add(current);
        }

        foreach (Vector2 current in uselessPlaces)
            _availablePlaces.Remove(current);
    }



    //-------------------------------Post Process Level Methods
    private void PostProcessingRooms()
    {
        CheckAllAvailablePlaces();
        AddBossRoom();

        AddItemRoom();

        for (int i = 0; i < _roomController.GetSecretRoomCount(); i++)
            AddSecretRoom();

        foreach (Room current in _levelCreated)
            current.InitializeRoom();
    }


    private void AddBossRoom()
    {
        Vector2 placeToUse = Vector2.zero;
        float distanceMin = 0f;

        //We want to find the farest room in the level
        foreach (Vector2 current in _availablePlaces)
        {
            if(_grid.FindPlaceNeighbour(Mathf.FloorToInt(current.x), Mathf.FloorToInt(current.y)) == 1)
            {
                //We compute its distance from origin
                float dSqrToTarget = Mathf.Sqrt((new Vector2(7, 7) - new Vector2(current.x, current.y)).sqrMagnitude);

                //And if it's more than previous we store it
                if (dSqrToTarget > distanceMin)
                {
                    distanceMin = dSqrToTarget;
                    placeToUse = current;
                }
            }
        }
        
        Room bufferRoom = Instantiate(_roomController.GetBossRoom());

        bufferRoom.SetX(Mathf.FloorToInt(placeToUse.x));
        bufferRoom.SetY(Mathf.FloorToInt(placeToUse.y));

        _grid.AddRoomToLevel(bufferRoom);

        bufferRoom.GetComponent<BossRoom>().InitializeRoom();
    }


    private void AddItemRoom()
    {
        Room roomToDestroy;
        do
            roomToDestroy = _levelCreated[Random.Range(0, _levelCreated.Count)];
        while (!roomToDestroy.GetCanBeChanged());

        Room roomToCreate = _roomController.GetItemRoom();

        _levelCreated.Add(_grid.ReplaceOldRoom(roomToDestroy, roomToCreate));
        _levelCreated.Remove(roomToDestroy);
    }


    private void AddSecretRoom()
    {
        Vector2 placeToUse = Vector2.zero;
        
        //We want to find the farest room in the level
        while(placeToUse == Vector2.zero)
        {
            Vector2 current = _availablePlaces[Random.Range(0, _availablePlaces.Count)];

            if (_grid.FindPlaceNeighbour(Mathf.FloorToInt(current.x), Mathf.FloorToInt(current.y)) >= 2)
                placeToUse = current;
        }

        _availablePlaces.Remove(placeToUse);

        Room bufferRoom = Instantiate(_roomController.GetSecretRoom());

        bufferRoom.SetX(Mathf.FloorToInt(placeToUse.x));
        bufferRoom.SetY(Mathf.FloorToInt(placeToUse.y));

        _grid.AddRoomToLevel(bufferRoom);
        _levelCreated.Add(bufferRoom);
    }



    //-------------------------------Getter
    public Vector3 GetSpawnPoint() { return _levelCreated[0].transform.position; }
}