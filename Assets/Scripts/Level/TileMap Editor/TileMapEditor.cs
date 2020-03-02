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
    private List<Tile> _tileAvailables = new List<Tile>();
    private Tile _tileRecovered;
    private Tile _backgroundTile;
    private Tile _leftSideTile;
    private Tile _rightSideTile;
    private Tile _upSideTile;
    private Tile _downSlideTile;
    Vector2 scrollPos;



    [MenuItem("Window/TileMapEditor")]
    public static void ShowWindow()
    {
        GetWindow<TileMapEditor>("Tile Map Editor");
    }


    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        if (GUILayout.Button("Import Data!", GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)))
            ImportData();

        if (GUILayout.Button("Export Data!", GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)))
            ExportData();

        _backgroundTile = (Tile)EditorGUILayout.ObjectField("BackGround Tile: ", _backgroundTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        _leftSideTile = (Tile)EditorGUILayout.ObjectField("Left Side Tile: ", _leftSideTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        _rightSideTile = (Tile)EditorGUILayout.ObjectField("Right Side Tile: ", _rightSideTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        _upSideTile = (Tile)EditorGUILayout.ObjectField("Up Side Tile: ", _upSideTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        _downSlideTile = (Tile)EditorGUILayout.ObjectField("Down Side Tile: ", _downSlideTile, typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));
        
        if (GUILayout.Button("Apply Background!", GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)))
            ApplyBackground();

       
        AddNewTileObjects();

        DrawTileMap();
        EditorGUILayout.EndScrollView();
    }


    private void AddNewTileObjects()
    {
        int newCount = Mathf.Max(0, EditorGUILayout.DelayedIntField("Number Of Tiles : ", _tileAvailables.Count, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false)));

        while (newCount < _tileAvailables.Count)
            _tileAvailables.RemoveAt(_tileAvailables.Count - 1);

        while (newCount > _tileAvailables.Count)
            _tileAvailables.Add(null);

        for (int i = 0; i < _tileAvailables.Count; i++)
            _tileAvailables[i] = (Tile)EditorGUILayout.ObjectField("Tile " + i + " : ", _tileAvailables[i], typeof(Tile), true, GUILayout.MinWidth(450), GUILayout.ExpandWidth(false));


        for (int i = 0; i < _tileAvailables.Count; i++)
        {
            int buffer = 0;

            if (Mathf.FloorToInt((float)i / 20f) <= 0)
                buffer = i;
            else
                buffer = i - 20;

            if (GUI.Button(new Rect(
                475 + buffer * _textureSize * 2f, 
                _textureSize + _textureSize * 2f * Mathf.FloorToInt((float)i / 20f), 
                _textureSize * 2, 
                _textureSize * 2),
                RecoverTextureFromTileSets(_tileAvailables[i]?.sprite), 
                GUIStyle.none))
                _tileRecovered = _tileAvailables[i];
        }
    }


    private void DrawTileMap()
    {
        for(int i = 0; i < _xSize; i ++)
        {
            for (int j = 0; j < _ySize; j++)
            {
                Sprite other = (_tiles[i, j]?.sprite == null)? Resources.Load<Sprite>("EmptyBorders") : _tiles[i, j].sprite;

                if (GUI.Button(
                    new Rect(475 + i * _textureSize, 150 +j * _textureSize, _textureSize, _textureSize), 
                    RecoverTextureFromTileSets(other), 
                    GUIStyle.none))
                    _tiles[i, j] = _tileRecovered;
            }
        }
    }


    private Texture2D RecoverTextureFromTileSets(Sprite other)
    {
        Texture2D croppedTexture = new Texture2D(_textureSize, _textureSize);
        Color[] pixels = other.texture.GetPixels((int)other.textureRect.x,
                                                (int)other.textureRect.y,
                                                _textureSize,
                                                _textureSize);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }


    private void ExportData()
    {
        List<Tile> bufferList = new List<Tile>(_tileAvailables);

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
            _tiles[i, 0] = _upSideTile;

        for (int i = 0; i < _ySize; i++)
            _tiles[0, i] = _leftSideTile;

        for (int i = 0; i < _xSize; i++)
            _tiles[i, _ySize - 1] = _downSlideTile;

        for (int i = 0; i < _ySize; i++)
            _tiles[_xSize - 1, i] = _rightSideTile;
    }


    private void ImportData()
    {
        foreach (GameObject current in Selection.gameObjects)
        {
            if (current.TryGetComponent(out TileMapGenerator newTileMap))
            {
                _tileAvailables = new List<Tile>(newTileMap.GetAvailableTiles());
                List<MultiDimensionnalArray> buffer = new List<MultiDimensionnalArray>(newTileMap.GetTileMapIndexes());

                for(int i = 0; i < _xSize; i ++)
                    for(int j = 0; j < _ySize; j ++)
                        _tiles[i, j] = _tileAvailables[buffer[i].GetValues()[j]];
            }
        }
    }
}