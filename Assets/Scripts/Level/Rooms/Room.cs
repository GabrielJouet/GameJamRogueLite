using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Parameters")]
    [SerializeField]
    protected Transform _cameraPosition;
    [SerializeField]
    protected bool _canBeChanged;


    [Header("Doors")]
    [SerializeField]
    protected Door _upDoor;
    [SerializeField]
    protected Door _leftDoor;
    [SerializeField]
    protected Door _downDoor;
    [SerializeField]
    protected Door _rightDoor;


    [Header("Enemies")]
    [SerializeField]
    protected List<GameObject> _roomEnemies = new List<GameObject>();


    [Header("UI")]
    [SerializeField]
    private Sprite _miniMapActiveIcon;
    [SerializeField]
    private Sprite _miniMapSeenIcon;
    [SerializeField]
    private Sprite _miniMapUnseenIcon;



    protected int _x, _y;
    protected LevelGrid _grid;
    
    protected bool _seen = false;

    

    //-------------------------------Initialization Methods
    public void InitializeRoom()
    {
        CheckNeighbours();

        foreach (GameObject current in _roomEnemies)
            current.SetActive(false);
    }


    protected void CheckNeighbours()
    {
        if(_grid == null)
            _grid = FindObjectOfType<LevelGrid>();

        if (!_grid.CheckRoomExistence(_x, _y + 1) && _upDoor != null)
            _upDoor.RemplaceByWall();
        
        if (!_grid.CheckRoomExistence(_x - 1, _y) && _leftDoor != null)
            _leftDoor.RemplaceByWall();
        
        if (!_grid.CheckRoomExistence(_x, _y - 1) && _downDoor != null)
            _downDoor.RemplaceByWall();
        
        if (!_grid.CheckRoomExistence(_x + 1, _y) && _rightDoor != null)
            _rightDoor.RemplaceByWall();
    }



    //-------------------------------Check Neighbours Methods
    public int GetDoorNumber()
    {
        int count = 0;

        count += _upDoor != null ? 1 : 0;
        count += _downDoor != null ? 1 : 0;
        count += _rightDoor != null ? 1 : 0;
        count += _leftDoor != null ? 1 : 0;

        return count;
    }

    
    public int GetNeighbourNumber()
    {
        int count = 0;
        
        if (_grid == null)
            _grid = FindObjectOfType<LevelGrid>();

        count += (_grid.CheckRoomExistence(_x, _y + 1) && _upDoor != null) ? 1 : 0;
        count += (_grid.CheckRoomExistence(_x - 1, _y) && _leftDoor != null) ? 1 : 0;
        count += (_grid.CheckRoomExistence(_x, _y - 1) && _downDoor != null) ? 1 : 0;
        count += (_grid.CheckRoomExistence(_x + 1, _y) && _rightDoor != null) ? 1 : 0;

        return count;
    }


    public List<Vector2> GetAllNeighbourPlaces()
    {
        List<Vector2> buffer = new List<Vector2>();

        if (_grid == null)
            _grid = FindObjectOfType<LevelGrid>();

        if (!_grid.CheckRoomExistence(_x, _y + 1) && _upDoor != null)
            buffer.Add(new Vector2(_x, _y + 1));

        if (!_grid.CheckRoomExistence(_x - 1, _y) && _leftDoor != null)
            buffer.Add(new Vector2(_x - 1, _y));

        if (!_grid.CheckRoomExistence(_x, _y - 1) && _downDoor != null)
            buffer.Add(new Vector2(_x, _y - 1));

        if (!_grid.CheckRoomExistence(_x + 1, _y) && _rightDoor != null)
            buffer.Add(new Vector2(_x + 1, _y));

        return buffer;
    }



    //-------------------------------Player Interaction Methods    
    public void RoomEntered()
    {
        _seen = true;
        /*
        foreach (GameObject current in _roomEnemies)
            current.GetComponent<Enemy>().PlayerEntered(this);
        
        //We close every doors
        if (_upDoor != null && _upDoor.GetCanBeClosed())
            _upDoor.Close();

        if (_downDoor != null && _downDoor.GetCanBeClosed())
            _downDoor.Close();

        if (_rightDoor != null && _rightDoor.GetCanBeClosed())
            _rightDoor.Close();

        if (_leftDoor != null && _leftDoor.GetCanBeClosed())
            _leftDoor.Close();
        */
    }



    //-------------------------------Enemies Interaction Methods
    public void EnemyKilled(GameObject other)
    {
        _roomEnemies.Remove(other);
        /*
        if (_roomEnemies.Count == 0)
            RoomFinished();
            */
    }



    //-------------------------------Setters
    public void SetX(int newX) { _x = newX; }

    public void SetY(int newY) { _y = newY; }



    //-------------------------------Getters
    public int GetX() { return _x; }

    public int GetY() { return _y; }

    public bool GetLeft() { return _leftDoor != null; }

    public bool GetRight() { return _rightDoor != null; }

    public bool GetUp() { return _upDoor != null; }

    public bool GetDown() { return _downDoor != null; }

    public bool GetCanBeChanged() { return _canBeChanged; }

    public Transform GetCameraPosition() { return _cameraPosition; }

    public Sprite GetMiniMapIcon() { return _seen? _miniMapSeenIcon : _miniMapUnseenIcon; }

    public Sprite GetActiveMiniMapIcon() { return _miniMapActiveIcon; }

    public bool GetSeen() { return _seen; }
}