using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class TileGridGenerator : EditorWindow
{
    public TileToPlace TilePrefab;
    public Road RoadPrefab;
    [FormerlySerializedAs("MaxX")] public int SizeX = 7;
    [FormerlySerializedAs("MaxZ")] public int SizeZ = 7;
    public int RandomRoadCount = 3;

    private int _roadZ;
    private List<Road> _randomRoads;
    private List<Road> _connectionRoads;
    
    [MenuItem("Tools/Tile Placer")]
    public static void ShowWindow()
    {
        GetWindow<TileGridGenerator>("Tile Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Tile Placer Settings", EditorStyles.boldLabel);

        TilePrefab = (TileToPlace)EditorGUILayout.ObjectField("Tile Prefab", TilePrefab, typeof(TileToPlace), false);
        RoadPrefab = (Road)EditorGUILayout.ObjectField("Road Prefab", RoadPrefab, typeof(Road), false);
        
        SizeX = EditorGUILayout.IntField("Max X", SizeX);
        SizeZ = EditorGUILayout.IntField("Max Z", SizeZ);
        RandomRoadCount = EditorGUILayout.IntField("Random Road Count", RandomRoadCount);

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
        _randomRoads = new List<Road>();

        // Початкова дорога
        _roadZ = Random.Range(-SizeZ + 1, SizeZ - 1);
        var startRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        startRoad.gameObject.name = "StartRoad";
        startRoad.transform.position = new Vector3(-SizeX, 0, _roadZ);
        startRoad.transform.parent = parent.transform;

        var currentRoadPosition = startRoad.transform.position;

        for (int i = 0; i < RandomRoadCount; i++)
        {
            // Обмеження для наступної позиції
            int minX = Mathf.Clamp((int)currentRoadPosition.x + 1, -SizeX + 1, SizeX - 1);
            int maxX = SizeX - 1;

            int minZ = -SizeZ + 1;
            int maxZ = SizeZ - 1;

            // Створення випадкової позиції в межах
            float randomX = Random.Range(minX, maxX + 1);
            float randomZ = Random.Range(minZ, maxZ + 1);

            var randomRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            randomRoad.gameObject.name = $"RandomRoad_{i}";
            randomRoad.transform.position = new Vector3(randomX, 0, randomZ);
            randomRoad.transform.parent = parent.transform;
            _randomRoads.Add(randomRoad);

            var targetPos = randomRoad.transform.position;

            // Горизонтальне з'єднання по X
            while (Mathf.Abs(currentRoadPosition.x - targetPos.x) > 0.1f)
            {
                var connectionRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
                connectionRoad.gameObject.name = "ConnectionRoad_X";
                connectionRoad.transform.position = currentRoadPosition;
                connectionRoad.transform.parent = parent.transform;

                var directionX = targetPos.x > currentRoadPosition.x ? 1 : -1;
                currentRoadPosition.x += directionX;
            }

            // Вертикальне з'єднання по Z
            while (Mathf.Abs(currentRoadPosition.z - targetPos.z) > 0.1f)
            {
                var connectionRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
                connectionRoad.gameObject.name = "ConnectionRoad_Z";
                connectionRoad.transform.position = currentRoadPosition;
                connectionRoad.transform.parent = parent.transform;

                var directionZ = targetPos.z > currentRoadPosition.z ? 1 : -1;
                currentRoadPosition.z += directionZ;
            }

            // Переносимо точку для наступної побудови
            currentRoadPosition = targetPos;
        }

        // Додаємо FinishRoad в кінець
        var finishZ = Random.Range(-SizeZ + 1, SizeZ - 1);
        var finishRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
        finishRoad.gameObject.name = "FinishRoad";
        finishRoad.transform.position = new Vector3(SizeX, 0, finishZ);
        finishRoad.transform.parent = parent.transform;

        var targetFinish = finishRoad.transform.position;

        // З'єднуємо останню RandomRoad з FinishRoad
        while (Mathf.Abs(currentRoadPosition.x - targetFinish.x) > 0.1f)
        {
            var connectionRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            connectionRoad.gameObject.name = "ConnectionRoad_X";
            connectionRoad.transform.position = currentRoadPosition;
            connectionRoad.transform.parent = parent.transform;

            var directionX = targetFinish.x > currentRoadPosition.x ? 1 : -1;
            currentRoadPosition.x += directionX;
        }

        while (Mathf.Abs(currentRoadPosition.z - targetFinish.z) > 0.1f)
        {
            var connectionRoad = (Road)PrefabUtility.InstantiatePrefab(RoadPrefab);
            connectionRoad.gameObject.name = "ConnectionRoad_Z";
            connectionRoad.transform.position = currentRoadPosition;
            connectionRoad.transform.parent = parent.transform;

            var directionZ = targetFinish.z > currentRoadPosition.z ? 1 : -1;
            currentRoadPosition.z += directionZ;
        }

        FillRemainingWithTiles(parent);

        Debug.Log("Tiles placed successfully!");
    }

    private void FillRemainingWithTiles(GameObject parent)
    {
        // Збираємо всі координати, де вже є дорога
        HashSet<Vector2> usedPositions = new HashSet<Vector2>();

        foreach (Transform child in parent.transform)
        {
            Vector3 pos = child.position;
            Vector2 gridPos = new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
            usedPositions.Add(gridPos);
        }

        for (int x = -SizeX; x <= SizeX; x++)
        {
            for (int z = -SizeZ; z <= SizeZ; z++)
            {
                Vector2 gridPos = new Vector2(x, z);

                if (!usedPositions.Contains(gridPos))
                {
                    var tile = (TileToPlace)PrefabUtility.InstantiatePrefab(TilePrefab);
                    tile.name = $"Tile_{x}_{z}";
                    tile.transform.position = new Vector3(x, 0, z);
                    tile.transform.parent = parent.transform;
                }
            }
        }
    }
}