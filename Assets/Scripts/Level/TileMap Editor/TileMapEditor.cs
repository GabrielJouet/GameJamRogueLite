using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapEditor : EditorWindow
{
    private readonly int _textureSize = 32;
    private readonly int _xSize = 26;
    private readonly int _ySize = 14;
    private Tile[,] _tiles = new Tile[26, 14];
    private List<MultiDimensionnalArray> _tileIndexes = new List<MultiDimensionnalArray>();
    private Tile _backgroundTile;
    private Tile _wallTile;
    Vector2 scrollPos;



    [MenuItem("Window/TileMapEditor")]
    public static void ShowWindow()
    {
        GetWindow<TileMapEditor>("Tile Map Editor");
    }


    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        if (GUILayout.Button("Export Data!", GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)))
            ExportData();

        _backgroundTile = (Tile)EditorGUILayout.ObjectField("BackGround Tile: ", _backgroundTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        _wallTile = (Tile)EditorGUILayout.ObjectField("Wall Tile: ", _wallTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        
        if (GUILayout.Button("Apply Background!", GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)))
            ApplyBackground();
        EditorGUILayout.EndScrollView();
    }


    private void ExportData()
    {
        List<Tile> bufferList = new List<Tile>
        {
            _wallTile,
            _backgroundTile
        };

        foreach (GameObject current in Selection.gameObjects)
        {
            if (current.TryGetComponent(out TileMapGenerator newTileMap))
            {
                _tileIndexes.Clear();
                for (int i = 0; i < _xSize; i++)
                {
                    _tileIndexes.Add(new MultiDimensionnalArray());
                    for (int j = 0; j < _ySize; j++)
                        _tileIndexes[i].AddValue(bufferList.IndexOf(_tiles[i, j]));
                }
                newTileMap.SetParameters(_tileIndexes, bufferList);
                EditorUtility.SetDirty(newTileMap);
            }
        }
    }


    private void ApplyBackground()
    {
        for(int i = 0; i< _xSize; i ++)
            for(int j = 0; j < _ySize; j ++)
                _tiles[i, j] = _backgroundTile;

        for(int i = 0; i < _xSize; i ++)
            _tiles[i, 0] = _wallTile;

        for (int i = 0; i < _ySize; i++)
            _tiles[0, i] = _wallTile;

        for (int i = 0; i < _xSize; i++)
            _tiles[i, _ySize - 1] = _wallTile;

        for (int i = 0; i < _ySize; i++)
            _tiles[_xSize - 1, i] = _wallTile;
    }
}