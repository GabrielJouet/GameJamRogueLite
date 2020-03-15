using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [Header("Tile Map Parameters")]
    [SerializeField]
    private List<Tile> _availableTiles;
    [SerializeField]
    private List<MultiDimensionnalArray> _tileMapIndexes;


    private Tilemap _tileMap;



    //-------------------------------Unity Methods
    private void Start()
    {
        GenerateTerrain();
    }



    //-------------------------------Tile map Generation Methods
    public void GenerateTerrain()
    {
        _tileMap = FindObjectOfType<Tilemap>();
        Grid neGrid = FindObjectOfType<Grid>();
        int x = 0;
        int y = 0;

        foreach(MultiDimensionnalArray current in _tileMapIndexes)
        {
            foreach(int other in current.GetValues())
            {
                Vector3 computedPosition = new Vector3(
                    transform.localPosition.x - (_tileMapIndexes.Count / 2 - x) * neGrid.cellSize.x,
                    transform.localPosition.y + (current.GetValues().Count / 2 - y - 1) * neGrid.cellSize.y,
                    0);

                Vector3Int cellPosition = _tileMap.LocalToCell(computedPosition);

                if (_tileMap.GetTile(cellPosition) == null)
                    _tileMap.SetTile(cellPosition, _availableTiles[other]);
                else
                    _tileMap.SetTile(_tileMap.LocalToCell(new Vector3(
                    transform.localPosition.x - (_tileMapIndexes.Count / 2 - x) * neGrid.cellSize.x + 0.01f,
                    transform.localPosition.y + (current.GetValues().Count / 2 - y - 1) * neGrid.cellSize.y,
                    0)), _availableTiles[other]);
                y++;
            }
            x++;

            y = 0;
        }
    }


    //-------------------------------Setters & Getters
    public void SetParameters(List<MultiDimensionnalArray> tileMapIndexes, List<Tile> currentTilesUsed)
    {
        _tileMapIndexes = new List<MultiDimensionnalArray>(tileMapIndexes);
        _availableTiles = new List<Tile>(currentTilesUsed);
    }


    public List<MultiDimensionnalArray> GetTileMapIndexes() { return _tileMapIndexes; }

    public List<Tile> GetAvailableTiles() { return _availableTiles; }
}