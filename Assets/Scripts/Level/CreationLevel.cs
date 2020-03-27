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


    [Header("Generation Texts")]
    [SerializeField]
    private List<string> _forestGenerationTexts;
    [SerializeField]
    private List<string> _templeGenerationTexts;
    [SerializeField]
    private List<string> _cavernGenerationTexts;
    [SerializeField]
    private List<string> _graveyardGenerationTexts;
    [SerializeField]
    private List<string> _endGenerationTexts;


    private Rooms _roomsUsed;

    private List<Room> _levelCreatedUsed = new List<Room>();
    private List<Room> _levelCreated = new List<Room>();
    private List<Vector2Int> _availablePlaces = new List<Vector2Int>();

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
        //Creating dungeon grid
        _grid.CreateGrid();
        yield return new WaitForFixedUpdate();

        //Creating forest level
        _text.text = _forestGenerationTexts[Random.Range(0, _forestGenerationTexts.Count)];
        CreateForestLevel();
        yield return new WaitUntil(() => _forestLevelGenerated);
        yield return new WaitForSeconds(0.5f);

        //Creating temple level
        _text.text = _templeGenerationTexts[Random.Range(0, _templeGenerationTexts.Count)];
        CreateTempleLevel();
        yield return new WaitUntil(() => _templeLevelGenerated);
        yield return new WaitForSeconds(0.5f);

        //Creating graveyard level
        _text.text = _graveyardGenerationTexts[Random.Range(0, _graveyardGenerationTexts.Count)];
        _availablePlaces.Shuffle();
        Vector2Int cavernGeneration = RecoverCavernGenerationPoint();
        CreateGraveyardLevel(RecoverGraveyardGenerationPoint());
        yield return new WaitUntil(() => _graveyardLevelGenerated);
        yield return new WaitForSeconds(0.5f);

        //Creating cavern level
        _text.text = _cavernGenerationTexts[Random.Range(0, _cavernGenerationTexts.Count)];
        CreateCavernLevel(cavernGeneration);
        yield return new WaitUntil(() => _caverLevelGenerated);
        yield return new WaitForSeconds(0.5f);

        //Initializing level
        _text.text = _endGenerationTexts[Random.Range(0, _endGenerationTexts.Count)];
        foreach (Room current in _levelCreated)
            current.InitializeRoom();
        _grid.GenerateMiniMap();
        _grid.MoveMiniMap(_levelCreated[0]);
        yield return new WaitForSeconds(0.5f);

        //Initializing player
        _generationUI.SetActive(false);
        SpawnPlayer();
    }


    private Vector2Int RecoverGraveyardGenerationPoint()
    {
        Vector2Int newPosition = _availablePlaces[0];
        foreach (Vector2Int current in _availablePlaces)
        {
            if (current.x < newPosition.x)
                newPosition = current;
        }

        return newPosition;
    }


    private Vector2Int RecoverCavernGenerationPoint()
    {
        Vector2Int newPosition = _availablePlaces[0];
        foreach (Vector2Int current in _availablePlaces)
        {
            if (current.x > newPosition.x)
                newPosition = current;
        }

        return newPosition;
    }


    private void CreateForestLevel()
    {
        _roomsUsed = _forestRooms;
        _levelCurrentlyGenerated = "Forest";
        StartCoroutine(GenerateLevel(new Vector2Int(_grid.GetXSize() / 2, 1)));
    }


    private void CreateTempleLevel()
    {
        _roomsUsed = _templeRooms;
        Vector2Int position = new Vector2Int();

        foreach(Vector2Int current in _availablePlaces)
        {
            if (current.y > position.y)
                position = current;
        }

        _levelCurrentlyGenerated = "Temple";
        StartCoroutine(GenerateLevel(position));
    }


    private void CreateGraveyardLevel(Vector2Int graveyardGenerationPoint)
    {
        _roomsUsed = _graveyardRooms;
        _levelCurrentlyGenerated = "Graveyard";
        StartCoroutine(GenerateLevel(graveyardGenerationPoint));
    }


    private void CreateCavernLevel(Vector2Int cavernGenerationPoint)
    {
        _roomsUsed = _cavernRooms;
        _levelCurrentlyGenerated = "Cavern";
        StartCoroutine(GenerateLevel(cavernGenerationPoint));
    }


    private IEnumerator GenerateLevel(Vector2Int generationStartPosition)
    {
        _levelCreatedUsed.Clear();
        _availablePlaces.Clear();

        InstantiateBaseRoom(generationStartPosition);
        yield return new WaitForFixedUpdate();

        for (int i = 0; i < _roomsUsed.GetRoomCount() + Mathf.RoundToInt(Random.Range(0, _roomsUsed.GetRoomCount() * 0.25f)); i++)
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

    
    private void InstantiateBaseRoom(Vector2Int generationStartPosition)
    {
        Room buffer = Instantiate(_roomsUsed.GetBaseRoom());

        //We put it at 7;0 (the down center)
        buffer.SetPosition(generationStartPosition);

        //We add this room to the grid, to available room and level created
        _grid.AddRoomToLevel(buffer);

        _levelCreated.Add(buffer);
        _levelCreatedUsed.Add(buffer);
        _availablePlaces.AddRange(buffer.GetAllAvailableNeighbourPlaces());
    }

    
    private void InstantiateBodyRoom()
    {
        Room chosenRoom = ChooseRoom();
        Room bufferRoom = Instantiate(chosenRoom);

        _availablePlaces.Remove(chosenRoom.GetPosition());
        bufferRoom.SetPosition(chosenRoom.GetPosition());

        _grid.AddRoomToLevel(bufferRoom);

        //We add it to level created and available rooms lists
        _levelCreated.Add(bufferRoom);
        _levelCreatedUsed.Add(bufferRoom);

        foreach (Vector2Int current in bufferRoom.GetAllAvailableNeighbourPlaces())
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
            Vector2Int bufferedPlace = FindOneAvailablePlace();

            buffer.SetPosition(bufferedPlace);
        }
        while (!_grid.FindRoomAvailability(buffer));

        return buffer;
    }


    private Vector2Int FindOneAvailablePlace()
    {
        Vector2Int buffer;
        bool result;

        do
        {
            buffer = _availablePlaces[Random.Range(0, _availablePlaces.Count)];

            result = _grid.FindPlaceAvailability(buffer);

            if (!result)
                _availablePlaces.Remove(buffer);
        }
        while (!result);

        return buffer;
    }


    private void CheckAllAvailablePlaces()
    {
        List<Vector2Int> uselessPlaces = new List<Vector2Int>();

        foreach (Vector2Int current in _availablePlaces)
        {
            if (!_grid.FindPlaceAvailability(current))
                uselessPlaces.Add(current);
        }

        foreach (Vector2Int current in uselessPlaces)
            _availablePlaces.Remove(current);
    }



    //-------------------------------Post Process Level Methods
    private void PostProcessingRooms()
    {
        CheckAllAvailablePlaces();
        AddBossRoom();
    }


    private void AddBossRoom()
    {
        Vector2Int placeToUse = Vector2Int.zero;
        float distanceMin = 0f;

        //We want to find the farest room in the level
        foreach (Vector2Int current in _availablePlaces)
        {
            if(_grid.FindPlaceNeighbour(current) == 1)
            {
                //We compute its distance from origin
                float dSqrToTarget = Mathf.Sqrt((_levelCreatedUsed[0].GetPosition() - current).sqrMagnitude);

                //And if it's more than previous we store it
                if (dSqrToTarget > distanceMin)
                {
                    distanceMin = dSqrToTarget;
                    placeToUse = current;
                }
            }
        }

        Room bufferRoom = Instantiate(_roomsUsed.GetBossRoom());

        bufferRoom.SetPosition(placeToUse);

        _availablePlaces.Remove(placeToUse);

        foreach (Vector2Int current in bufferRoom.GetAllAvailableNeighbourPlaces())
        {
            if (!_availablePlaces.Contains(current))
                _availablePlaces.Add(current);
        }

        _grid.AddRoomToLevel(bufferRoom);
        _levelCreated.Add(bufferRoom);
    }



    private void SpawnPlayer()
    {
        TransitionSaver saver = FindObjectOfType<TransitionSaver>();

        PlayerMovement newPlayer = Instantiate(saver.GetPlayer(), _levelCreated[0].transform.position, Quaternion.identity);
        newPlayer.Initialize(saver, FindObjectOfType<PlayerUI>(), false);

        transform.position = new Vector3(_levelCreated[0].transform.position.x, _levelCreated[0].transform.position.y, -10);
        GetComponent<FollowCamera>().FollowPlayer(transform.position);

        _levelCreated[0].RoomEntered();
    }
}