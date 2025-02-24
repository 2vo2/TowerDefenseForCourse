using UnityEditor;
using UnityEngine;

public class TilePlacer : EditorWindow
{
    public Tile TilePrefab;
    public int MinX = -7;
    public int MaxX = 7;
    public int MinZ = -7;
    public int MaxZ = 7;

    [MenuItem("Tools/Tile Placer")]
    public static void ShowWindow()
    {
        GetWindow<TilePlacer>("Tile Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Placer Settings", EditorStyles.boldLabel);

        TilePrefab = (Tile)EditorGUILayout.ObjectField("Tile Prefab", TilePrefab, typeof(Tile), false);

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

        for (var x = MinX; x <= MaxX; x++)
        {
            for (var z = MinZ; z <= MaxZ; z++)
            {
                var position = new Vector3(x, 0, z);
                var tile = (Tile)PrefabUtility.InstantiatePrefab(TilePrefab);
                tile.transform.position = position;
                tile.transform.parent = parent.transform;
            }
        }

        Debug.Log("Tiles placed successfully!");
    }
}