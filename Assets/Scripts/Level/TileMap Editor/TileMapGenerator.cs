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

    
    [Header("Generation position")]
    [SerializeField]
    private Transform _tileMapStartPoint;
    private float _initialYPosition;


    private Tilemap _tileMap;



    //-------------------------------Unity Methods
    private void Start()
    {
        GenerateTerrain();
    }



    //-------------------------------Tile map Generation Methods
    public void GenerateTerrain()
    {
        _initialYPosition = _tileMapStartPoint.localPosition.y;
        _tileMap = FindObjectOfType<Tilemap>();

        foreach(MultiDimensionnalArray current in _tileMapIndexes)
        {
            foreach(int other in current.GetValues())
            {
                _tileMap.SetTile(_tileMap.LocalToCell(_tileMapStartPoint.position), _availableTiles[other]);
                _tileMapStartPoint.localPosition = new Vector3(_tileMapStartPoint.localPosition.x, _tileMapStartPoint.localPosition.y - 0.32f, _tileMapStartPoint.localPosition.z);
            }
            _tileMapStartPoint.localPosition = new Vector3(_tileMapStartPoint.localPosition.x + 0.32f, _initialYPosition, _tileMapStartPoint.localPosition.z);
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