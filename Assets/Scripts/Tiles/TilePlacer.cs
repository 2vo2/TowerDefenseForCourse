using UnityEditor;
using UnityEngine;

public class TilePlacer : EditorWindow
{
    public TileToPlace TilePrefab;
    public Road RoadPrefab;
    public int MinX = -7;
    public int MaxX = 7;
    public int MinZ = -7;
    public int MaxZ = 7;

    private int _roadZ;
    
    [MenuItem("Tools/Tile Placer")]
    public static void ShowWindow()
    {
        GetWindow<TilePlacer>("Tile Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Placer Settings", EditorStyles.boldLabel);

        TilePrefab = (TileToPlace)EditorGUILayout.ObjectField("Tile Prefab", TilePrefab, typeof(TileToPlace), false);
        RoadPrefab = (Road)EditorGUILayout.ObjectField("Road Prefab", RoadPrefab, typeof(Road), false);

        MinX = EditorGUILayout.IntField("Min X", MinX);
        MaxX = EditorGUILayout.IntField("Max X", MaxX);
        MinZ = EditorGUILayout.IntField("Min Z", MinZ);
        MaxZ = EditorGUILayout.IntField("Max Z", MaxZ);

        if (GUILayout.Button("Place Tiles"))
        {
            PlaceTiles();
        }
    }

    private void PlaceTiles()
    {
        if (TilePrefab == null)
        {
            Debug.LogError("Assign a tile prefab first!");
            return;
        }

        var parent = new GameObject("TileGrid");
        
        _roadZ = Random.Range(MinZ, MaxZ + 1);
        
        for (var x = MinX; x <= MaxX; x++)
        {
            for (var z = MinZ; z <= MaxZ; z++)
            {
                var position = new Vector3(x, 0, z);

                if (z == _roadZ)
                {
                    var road = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
                    road.transform.position = position;
                    road.transform.parent = parent.transform;
                }
                else
                {
                    var tile = (TileToPlace)PrefabUtility.InstantiatePrefab(TilePrefab);
                    tile.transform.position = position;
                    tile.transform.parent = parent.transform;
                }
            }
        }

        Debug.Log("Tiles placed successfully!");
    }
}