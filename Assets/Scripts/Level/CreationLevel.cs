using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationLevel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private LevelGrid _grid;
    [SerializeField]
    private Rooms _forestRooms;
    [SerializeField]
    private Rooms _templeRooms;
    [SerializeField]
    private Rooms _cavernRooms;
    [SerializeField]
    private Rooms _graveyardRooms;


    [Header("UI")]
    [SerializeField]
    private Text _text;
    [SerializeField]
    private GameObject _generationUI;


    private Rooms _roomsUsed;

    private List<Room> _levelCreatedUsed = new List<Room>();
    private List<Room> _levelCreated = new List<Room>();
    private List<Vector2> _availablePlaces = new List<Vector2>();

    private Vector2 _graveyardGenerationPoint;
    private Vector2 _cavernGenerationPoint;

    private bool _forestLevelGenerated = false;
    private bool _templeLevelGenerated = false;
    private bool _caverLevelGenerated = false;
    private bool _graveyardLevelGenerated = false;
    private string _levelCurrentlyGenerated;


    //-------------------------------Creation Base Level Methods
    private void Start()
    {
        StartCoroutine(CreateDungeon());
    }


    private IEnumerator CreateDungeon()
    {
        _grid.CreateGrid();
        yield return new WaitForFixedUpdate();
        _text.text = "Magic tree creation...";

        //We create every forest room
        CreateForestLevel();
        yield return new WaitUntil(() => _forestLevelGenerated);
        _text.text = "Building marble tiles...";

        //We create every temple room
        CreateTempleLevel();
        yield return new WaitUntil(() => _templeLevelGenerated);
        _text.text = "Killing humans...";

        _availablePlaces.Shuffle();
        _graveyardGenerationPoint = _availablePlaces[0];
        foreach (Vector2 current in _availablePlaces)
        {
            if (current.x < _graveyardGenerationPoint.x)
                _graveyardGenerationPoint = current;
        }

        _cavernGenerationPoint = _availablePlaces[0];
        foreach (Vector2 current in _availablePlaces)
        {
            if (current.x > _cavernGenerationPoint.x)
                _cavernGenerationPoint = current;
        }

        //We create every graveyard room
        CreateGraveyardLevel();
        yield return new WaitUntil(() => _graveyardLevelGenerated);
        _text.text = "Carving gems...";

        //We create every cavern room
        CreateCavernLevel();
        yield return new WaitUntil(() => _caverLevelGenerated);
        _text.text = "Awake monsters...";

        foreach (Room current in _levelCreated)
            current.InitializeRoom();
        yield return new WaitForSeconds(0.5f);
        _generationUI.SetActive(false);

        Instantiate(FindObjectOfType<TransitionSaver>().GetPlayer(), _levelCreated[0].transform.position, Quaternion.identity);
        transform.position = new Vector3(_levelCreated[0].transform.position.x, _levelCreated[0].transform.position.y, -10);
        FindObjectOfType<FollowCamera>().FollowPlayer(transform.position);
    }


    private void CreateForestLevel()
    {
        _roomsUsed = _forestRooms;
        _levelCurrentlyGenerated = "Forest";
        StartCoroutine(GenerateLevel(new Vector2(_grid.GetXSize() / 2, 1)));
    }


    private void CreateTempleLevel()
    {
        _roomsUsed = _templeRooms;
        Vector2 position = new Vector2();

        foreach(Vector2 current in _availablePlaces)
        {
            if (current.y > position.y)
                position = current;
        }

        _levelCurrentlyGenerated = "Temple";
        StartCoroutine(GenerateLevel(position));
    }


    private void CreateGraveyardLevel()
    {
        _roomsUsed = _graveyardRooms;
        _levelCurrentlyGenerated = "Graveyard";
        StartCoroutine(GenerateLevel(_graveyardGenerationPoint));
    }


    private void CreateCavernLevel()
    {
        _roomsUsed = _cavernRooms;
        _levelCurrentlyGenerated = "Cavern";
        StartCoroutine(GenerateLevel(_cavernGenerationPoint));
    }


    private IEnumerator GenerateLevel(Vector2 generationStartPosition)
    {
        _levelCreatedUsed.Clear();
        _availablePlaces.Clear();

        InstantiateBaseRoom(generationStartPosition);
        yield return new WaitForFixedUpdate();

        int roomCount = _roomsUsed.GetRoomCount() + Mathf.RoundToInt(Random.Range(0, _roomsUsed.GetRoomCount() * 0.25f));

        for (int i = 0; i < roomCount; i++)
        {
            InstantiateBodyRoom();
            if(i % 10 == 0)
                yield return new WaitForFixedUpdate();
        }

        PostProcessingRooms();
        yield return new WaitForFixedUpdate();

        switch(_levelCurrentlyGenerated)
        {
            case "Forest":
                _forestLevelGenerated = true;
                break;
            case "Temple":
                _templeLevelGenerated = true;
                break;
            case "Cavern":
                _caverLevelGenerated = true;
                break;
            case "Graveyard":
                _graveyardLevelGenerated = true;
                break;
        }
    }

    
    private void InstantiateBaseRoom(Vector2 generationStartPosition)
    {
        Room buffer = Instantiate(_roomsUsed.GetBaseRoom());

        //We put it at 7;0 (the down center)
        buffer.SetX(Mathf.FloorToInt(generationStartPosition.x));
        buffer.SetY(Mathf.FloorToInt(generationStartPosition.y));

        //We add this room to the grid, to available room and level created
        _grid.AddRoomToLevel(buffer);

        _levelCreated.Add(buffer);
        _levelCreatedUsed.Add(buffer);
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
        _levelCreatedUsed.Add(bufferRoom);

        foreach (Vector2 current in bufferRoom.GetAllNeighbourPlaces())
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
            buffer = _roomsUsed.GetBodyRoom();
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

        for (int i = 0; i < _roomsUsed.GetSecretRoomCount(); i++)
            AddSecretRoom();
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
        
        Room bufferRoom = Instantiate(_roomsUsed.GetBossRoom());

        bufferRoom.SetX(Mathf.FloorToInt(placeToUse.x));
        bufferRoom.SetY(Mathf.FloorToInt(placeToUse.y));

        _grid.AddRoomToLevel(bufferRoom);

        bufferRoom.GetComponent<BossRoom>().InitializeRoom();
    }


    private void AddItemRoom()
    {
        Room roomToDestroy;
        do
            roomToDestroy = _levelCreatedUsed[Random.Range(0, _levelCreatedUsed.Count)];
        while (!roomToDestroy.GetCanBeChanged());

        Room roomToCreate = _roomsUsed.GetItemRoom();
        Room bufferRoom = _grid.ReplaceOldRoom(roomToDestroy, roomToCreate);

        _levelCreated.Add(bufferRoom);
        _levelCreatedUsed.Add(bufferRoom);
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

        Room bufferRoom = Instantiate(_roomsUsed.GetSecretRoom());

        bufferRoom.SetX(Mathf.FloorToInt(placeToUse.x));
        bufferRoom.SetY(Mathf.FloorToInt(placeToUse.y));

        _grid.AddRoomToLevel(bufferRoom);
        _levelCreated.Add(bufferRoom);
        _levelCreatedUsed.Add(bufferRoom);
    }



    //-------------------------------Getter
    public Vector3 GetSpawnPoint() { return _levelCreated[0].transform.position; }
}