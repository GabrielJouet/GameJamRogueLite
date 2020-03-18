using UnityEngine;
using UnityEngine.UI;

public class LevelGrid : MonoBehaviour
{
    [Header("Level Parameters")]
    [SerializeField]
    [Range(5, 100)]
    private int _xSizeMax;
    [SerializeField]
    [Range(5, 100)]
    private int _ySizeMax;


    [SerializeField]
    private RectTransform _miniMap;
    [SerializeField]
    private RectTransform _roomIcon;
    

    private Room[,] _grid;
    private Image[,] _miniMapGrid;


    //-------------------------------Grid Creation Methods
    public void CreateGrid()
    {
        _grid = new Room[_xSizeMax, _ySizeMax];
    }



    //-------------------------------Grid Alteration Methods
    public void AddRoomToLevel(Room room)
    {
        _grid[room.GetPosition().x, room.GetPosition().y] = room;

        room.transform.position = new Vector3(3.84f * (room.GetPosition().x - Mathf.FloorToInt(_xSizeMax/2f)), 
                                              2.56f * (room.GetPosition().y - Mathf.FloorToInt(_ySizeMax / 2f)), 
                                              0f);
    }

    
    public void RemoveRoomToLevel(Room room)
    {
        _grid[room.GetPosition().x, room.GetPosition().y] = null;
    }


    public Room ReplaceOldRoom(Room oldRoom, Room newRoom)
    {
        RemoveRoomToLevel(oldRoom);

        Room buffer = Instantiate(newRoom);

        buffer.SetPosition(oldRoom.GetPosition());

        Destroy(oldRoom.gameObject);

        AddRoomToLevel(buffer);

        return buffer;
    }



    //-------------------------------Check Place Methods
    public bool FindRoomAvailability(Room room)
    {
        Vector2Int roomPosition = room.GetPosition();

        //If the room is within the grid
        if (roomPosition.y - 1 < _ySizeMax && roomPosition.y - 1 > 0)
        {            
            //If the room location is already occupied
            if (_grid[roomPosition.x, roomPosition.y - 1] != null)
            {
                //If the neighbour room has a up door but this room does not
                if (!(room.GetDown() && _grid[roomPosition.x, roomPosition.y - 1].GetUp()))
                    return false;
            }
        }
        else
            return false;
        
        if (roomPosition.x - 1 < _xSizeMax && roomPosition.x - 1 > 0)
        {
            if (_grid[roomPosition.x - 1, roomPosition.y] != null)
            {
                if (!(room.GetLeft() && _grid[roomPosition.x - 1, roomPosition.y].GetRight()))
                    return false;
            }
        }
        else
            return false;
        
        if (roomPosition.x + 1 < _xSizeMax && roomPosition.x + 1 > 0)
        {
            if (_grid[roomPosition.x + 1, roomPosition.y] != null)
            {
                if (!(room.GetRight() && _grid[roomPosition.x + 1, roomPosition.y].GetLeft()))
                    return false;
            }
        }
        else
            return false;
        
        if (roomPosition.y + 1 < _ySizeMax && roomPosition.y + 1 > 0)
        {
            if (_grid[roomPosition.x, roomPosition.y + 1] != null)
            {
                if (!(room.GetUp() && _grid[roomPosition.x, roomPosition.y + 1].GetDown()))
                    return false;
            }
        }
        else
            return false;
            

        return true;
    }

   
    public bool CheckRoomPosition(Vector2Int position)
    {
        if (position.x >= _xSizeMax || position.x < 0)
            return false;
        else if (position.y >= _ySizeMax || position.y < 0)
            return false;

        if (_grid[position.x, position.y] != null)
            return false;

        return true;
    }

    
    public bool CheckRoomExistence(Vector2Int position)
    {
        if (position.x >= _xSizeMax || position.y >= _ySizeMax || position.x < 0 || position.y < 0)
            return false;

        if (_grid[position.x, position.y] == null)
            return false;
        else
            return true;
    }


    public bool FindPlaceAvailability(Vector2Int position)
    {
        bool result = true && CheckRoomPosition(position);

        if (position.x - 1 < 0 || position.x + 1 >= _xSizeMax || position.y - 1 < 0 || position.y + 1 >= _ySizeMax)
            return false;


        if(_grid[position.x + 1, position.y] != null)
            if (!_grid[position.x + 1, position.y].GetLeft())
                result = false;

        if (_grid[position.x - 1, position.y] != null)
            if (!_grid[position.x - 1, position.y].GetRight())
                result = false;

        if (_grid[position.x, position.y + 1] != null)
            if (!_grid[position.x, position.y + 1].GetDown())
                result = false;

        if (_grid[position.x, position.y - 1] != null)
            if (!_grid[position.x, position.y - 1].GetUp())
                result = false;

        return result;
    }


    public int FindPlaceNeighbour(Vector2Int position)
    {
        int count = 0;

        if (position.x - 1 > 0)
            count += _grid[position.x - 1, position.y] != null ? 1 : 0;

        if (position.x + 1 < _xSizeMax)
            count += _grid[position.x + 1, position.y] != null ? 1 : 0;

        if (position.y - 1 > 0)
            count += _grid[position.x, position.y - 1] != null ? 1 : 0;

        if (position.y + 1 < _ySizeMax)
            count += _grid[position.x, position.y + 1] != null ? 1 : 0;

        return count;
    }


    public void GenerateMiniMap()
    {
        _miniMapGrid = new Image[_xSizeMax, _ySizeMax];

        for (int x = 0; x < _xSizeMax; x ++)
        {
            for(int y = 0; y < _ySizeMax; y ++)
            {
                if(CheckRoomExistence(new Vector2Int(x,y)))
                {
                    RectTransform newIcon = Instantiate(_roomIcon, _miniMap);
                    newIcon.localPosition = new Vector3(
                        (_roomIcon.sizeDelta.x + 5) * x,
                        (_roomIcon.sizeDelta.y + 5) * y,
                        0);

                    _miniMapGrid[x, y] = newIcon.GetComponent<Image>();
                    newIcon.gameObject.SetActive(false);
                }
            }
        }
    }


    public void MoveMiniMap(Room position)
    {
        Vector2Int roomPosition = position.GetPosition();
        _miniMap.localPosition = new Vector3(
            -(_roomIcon.sizeDelta.x + 5) * roomPosition.x,
            -(_roomIcon.sizeDelta.y + 5) * roomPosition.y,
            0);

        _miniMapGrid[roomPosition.x, roomPosition.y].gameObject.SetActive(true);
        _miniMapGrid[roomPosition.x, roomPosition.y].sprite = position.GetActiveMiniMapIcon();

        foreach (Vector2Int current in position.GetAllNeighbourPlaces()) 
            ActivateMiniMapIcon(current);
    }


    private void ActivateMiniMapIcon(Vector2Int position)
    {
        _miniMapGrid[position.x, position.y].sprite = GetRoomAtPoint(position).GetMiniMapIcon();
        _miniMapGrid[position.x, position.y].gameObject.SetActive(true);
    }


    public void DesactivateNeighboursRooms(Room other)
    {
        foreach(Vector2Int current in other.GetAllNeighbourPlaces())
            GetRoomAtPoint(current)?.DesactivateElements();
    }



    //-------------------------------Getters
    public int GetXSize() { return _xSizeMax; }

    public int GetYSize() { return _ySizeMax; }

    public Room GetRoomAtPoint(Vector2Int position) { return _grid[position.x, position.y]; }
}