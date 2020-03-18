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
        _grid[room.GetX(), room.GetY()] = room;

        room.transform.position = new Vector3(3.84f * (room.GetX() - Mathf.FloorToInt(_xSizeMax/2f)), 2.56f * (room.GetY() - Mathf.FloorToInt(_ySizeMax / 2f)), 0f);
    }

    
    public void RemoveRoomToLevel(Room room)
    {
        _grid[room.GetX(), room.GetY()] = null;
    }


    public Room ReplaceOldRoom(Room oldRoom, Room newRoom)
    {
        RemoveRoomToLevel(oldRoom);

        Room buffer = Instantiate(newRoom);

        buffer.SetX(oldRoom.GetX());
        buffer.SetY(oldRoom.GetY());

        Destroy(oldRoom.gameObject);

        AddRoomToLevel(buffer);

        return buffer;
    }



    //-------------------------------Check Place Methods
    public bool FindRoomAvailability(Room room)
    {   
        //If the room is within the grid
        if(room.GetY() - 1 < _ySizeMax && room.GetY() - 1 > 0)
        {            
            //If the room location is already occupied
            if (_grid[room.GetX(), room.GetY() - 1] != null)
            {
                //If the neighbour room has a up door but this room does not
                if (!(room.GetDown() && _grid[room.GetX(), room.GetY() - 1].GetUp()))
                    return false;
            }
        }
        else
            return false;
        
        if (room.GetX() - 1 < _xSizeMax && room.GetX() - 1 > 0)
        {
            if (_grid[room.GetX() - 1, room.GetY()] != null)
            {
                if (!(room.GetLeft() && _grid[room.GetX() - 1, room.GetY()].GetRight()))
                    return false;
            }
        }
        else
            return false;
        
        if (room.GetX() + 1 < _xSizeMax && room.GetX() + 1 > 0)
        {
            if (_grid[room.GetX() + 1, room.GetY()] != null)
            {
                if (!(room.GetRight() && _grid[room.GetX() + 1, room.GetY()].GetLeft()))
                    return false;
            }
        }
        else
            return false;
        
        if (room.GetY() + 1 < _ySizeMax && room.GetY() + 1 > 0)
        {
            if (_grid[room.GetX(), room.GetY() + 1] != null)
            {
                if (!(room.GetUp() && _grid[room.GetX(), room.GetY() + 1].GetDown()))
                    return false;
            }
        }
        else
            return false;
            

        return true;
    }

   
    public bool CheckRoomPosition(int xPos, int yPos)
    {
        if (xPos >= _xSizeMax || xPos < 0)
            return false;
        else if (yPos >= _ySizeMax || yPos < 0)
            return false;

        if (_grid[xPos, yPos] != null)
            return false;

        return true;
    }

    
    public bool CheckRoomExistence(int xPos, int yPos)
    {
        if (xPos >= _xSizeMax || yPos >= _ySizeMax || xPos < 0 || yPos < 0)
            return false;

        if (_grid[xPos, yPos] == null)
            return false;
        else
            return true;
    }


    public bool FindPlaceAvailability(int xPos, int yPos)
    {
        bool result = true && CheckRoomPosition(xPos, yPos);

        if (xPos - 1 < 0 || xPos + 1 >= _xSizeMax || yPos - 1 < 0 || yPos + 1 >= _ySizeMax)
            return false;


        if(_grid[xPos + 1, yPos] != null)
            if (!_grid[xPos + 1, yPos].GetLeft())
                result = false;

        if (_grid[xPos - 1, yPos] != null)
            if (!_grid[xPos - 1, yPos].GetRight())
                result = false;

        if (_grid[xPos, yPos + 1] != null)
            if (!_grid[xPos, yPos + 1].GetDown())
                result = false;

        if (_grid[xPos, yPos - 1] != null)
            if (!_grid[xPos, yPos - 1].GetUp())
                result = false;

        return result;
    }


    public int FindPlaceNeighbour(int xPos, int yPos)
    {
        int count = 0;

        if (xPos - 1 > 0)
            count += _grid[xPos - 1, yPos] != null ? 1 : 0;

        if (xPos + 1 < _xSizeMax)
            count += _grid[xPos + 1, yPos] != null ? 1 : 0;

        if (yPos - 1 > 0)
            count += _grid[xPos, yPos - 1] != null ? 1 : 0;

        if (xPos + 1 < _ySizeMax)
            count += _grid[xPos, yPos + 1] != null ? 1 : 0;

        return count;
    }


    public void GenerateMiniMap()
    {
        _miniMapGrid = new Image[_xSizeMax, _ySizeMax];

        for (int x = 0; x < _xSizeMax; x ++)
        {
            for(int y = 0; y < _ySizeMax; y ++)
            {
                if(CheckRoomExistence(x,y))
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
        _miniMap.localPosition = new Vector3(
            -(_roomIcon.sizeDelta.x + 5) * position.GetX(),
            -(_roomIcon.sizeDelta.y + 5) * position.GetY(),
            0);

        _miniMapGrid[position.GetX(), position.GetY()].gameObject.SetActive(true);
        _miniMapGrid[position.GetX(), position.GetY()].sprite = position.GetActiveMiniMapIcon();

        ActivateMiniMapIcon(new Vector2Int(position.GetX() + 1, position.GetY()));
        ActivateMiniMapIcon(new Vector2Int(position.GetX() - 1, position.GetY()));
        ActivateMiniMapIcon(new Vector2Int(position.GetX(), position.GetY() + 1));
        ActivateMiniMapIcon(new Vector2Int(position.GetX(), position.GetY() - 1));
    }


    private void ActivateMiniMapIcon(Vector2Int position)
    {
        if (CheckRoomExistence(position.x, position.y))
        {
            _miniMapGrid[position.x, position.y].sprite = GetRoomAtPoint(position.x, position.y).GetMiniMapIcon();
            _miniMapGrid[position.x, position.y].gameObject.SetActive(true);
        }
    }


    public void DesactivateNeighboursRooms(Room other)
    {
        if(CheckRoomExistence(other.GetX() + 1, other.GetY()))
            GetRoomAtPoint(other.GetX() + 1, other.GetY()).DesactivateElements();

        if (CheckRoomExistence(other.GetX() - 1, other.GetY()))
            GetRoomAtPoint(other.GetX() - 1, other.GetY()).DesactivateElements();

        if (CheckRoomExistence(other.GetX(), other.GetY() - 1))
            GetRoomAtPoint(other.GetX(), other.GetY() - 1).DesactivateElements();

        if (CheckRoomExistence(other.GetX(), other.GetY() + 1))
            GetRoomAtPoint(other.GetX(), other.GetY() + 1).DesactivateElements();
    }



    //-------------------------------Getters
    public int GetXSize() { return _xSizeMax; }

    public int GetYSize() { return _ySizeMax; }

    public Room GetRoomAtPoint(int x, int y) { return _grid[x, y]; }
}