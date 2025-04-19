using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TileGridGenerator : EditorWindow
{
    [SerializeField] private TileToPlace TilePrefab;
    [SerializeField] private Road RoadPrefab;
    [SerializeField] private int SizeX = 10;
    [SerializeField] private int SizeZ = 10;

    private List<Road> _randomRoads;

    [MenuItem("Tools/Tile Grid Generator")]
    public static void ShowWindow()
    {
        GetWindow<TileGridGenerator>("Tile Grid Generator");
    }

    private void OnGUI()
    {
        TilePrefab = (TileToPlace)EditorGUILayout.ObjectField("Tile Prefab", TilePrefab, typeof(TileToPlace), false);
        RoadPrefab = (Road)EditorGUILayout.ObjectField("Road Prefab", RoadPrefab, typeof(Road), false);
        SizeX = EditorGUILayout.IntField("Size X", SizeX);
        SizeZ = EditorGUILayout.IntField("Size Z", SizeZ);

        if (GUILayout.Button("Generate Grid"))
        {
            PlaceTiles();
        }
    }

    private void PlaceTiles()
    {
        if (TilePrefab == null || RoadPrefab == null)
        {
            Debug.LogError("Assign prefabs first!");
            return;
        }

        var parent = new GameObject("TileGrid");
        _randomRoads = new List<Road>();

        int halfInnerX = Mathf.CeilToInt((SizeX - 1) / 2f);
        int innerZ = SizeZ - 1;

        // Старт
        int startZ = Random.Range(-innerZ, innerZ + 1);
        var startRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        startRoad.name = "StartRoad";
        startRoad.transform.position = new Vector3(-SizeX, 0, startZ);
        startRoad.transform.parent = parent.transform;

        // Фініш
        int finishZ = Random.Range(-innerZ, innerZ + 1);
        var finishRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        finishRoad.name = "FinishRoad";
        finishRoad.transform.position = new Vector3(SizeX, 0, finishZ);
        finishRoad.transform.parent = parent.transform;

        // Випадкові дороги з унікальним Z
        HashSet<int> usedZ = new HashSet<int>();

        Vector3 leftRandomPos;
        do
        {
            int randX = Random.Range(-SizeX + 1, -SizeX + 1 + halfInnerX);
            int randZ = Random.Range(-innerZ, innerZ + 1);
            leftRandomPos = new Vector3(randX, 0, randZ);
        } while (!usedZ.Add((int)leftRandomPos.z));

        Vector3 rightRandomPos;
        do
        {
            int randX = Random.Range(-SizeX + 1 + halfInnerX, SizeX);
            int randZ = Random.Range(-innerZ, innerZ + 1);
            rightRandomPos = new Vector3(randX, 0, randZ);
        } while (!usedZ.Add((int)rightRandomPos.z));

        // Створення випадкових доріг
        var leftRandom = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        leftRandom.name = "LeftRandomRoad";
        leftRandom.transform.position = leftRandomPos;
        leftRandom.transform.parent = parent.transform;

        var rightRandom = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        rightRandom.name = "RightRandomRoad";
        rightRandom.transform.position = rightRandomPos;
        rightRandom.transform.parent = parent.transform;

        // З’єднання Start → LeftRandom → RightRandom → Finish
        ConnectPoints(startRoad.transform.position, leftRandomPos, parent);
        ConnectPoints(leftRandomPos, rightRandomPos, parent);

        // Спочатку до передфінішу (по X), не на SizeX
        Vector3 preFinish = new Vector3(SizeX - 1, 0, finishZ);
        ConnectPoints(rightRandomPos, preFinish, parent);

        // Тепер від preFinish до Finish (по Z)
        while (Mathf.Abs(preFinish.z - finishZ) > 0.1f)
        {
            var connection = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            connection.name = "FinalConnection_Z";
            connection.transform.position = preFinish;
            connection.transform.parent = parent.transform;

            preFinish.z += finishZ > preFinish.z ? 1 : -1;
        }

        FillRemainingWithTiles(parent);
        Debug.Log("Tiles placed successfully!");
    }

    private void ConnectPoints(Vector3 from, Vector3 to, GameObject parent)
    {
        Vector3 current = from;

        while (Mathf.Abs(current.x - to.x) > 0.1f)
        {
            current.x += current.x < to.x ? 1 : -1;

            var road = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            road.name = "Connection_X";
            road.transform.position = current;
            road.transform.parent = parent.transform;
        }

        while (Mathf.Abs(current.z - to.z) > 0.1f)
        {
            current.z += current.z < to.z ? 1 : -1;

            var road = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            road.name = "Connection_Z";
            road.transform.position = current;
            road.transform.parent = parent.transform;
        }
    }

    private void FillRemainingWithTiles(GameObject parent)
    {
        HashSet<Vector2Int> occupied = new HashSet<Vector2Int>();
        foreach (Transform child in parent.transform)
        {
            Vector3 pos = child.position;
            Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
            occupied.Add(gridPos);
        }

        for (int x = -SizeX; x <= SizeX; x++)
        {
            for (int z = -SizeZ; z <= SizeZ; z++)
            {
                Vector2Int pos = new Vector2Int(x, z);
                if (occupied.Contains(pos)) continue;

                var tile = (TileToPlace)PrefabUtility.InstantiatePrefab(TilePrefab);
                tile.name = $"Tile_{x}_{z}";
                tile.transform.position = new Vector3(x, 0, z);
                tile.transform.parent = parent.transform;
            }
        }
    }
}
