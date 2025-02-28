using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
