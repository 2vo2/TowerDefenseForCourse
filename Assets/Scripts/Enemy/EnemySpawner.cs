using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _spawnSize;
    [SerializeField] private float _spawnDelay;

    private List<Enemy> _enemies = new List<Enemy>();

    public List<Enemy> Enemies => _enemies;
    public event UnityAction<Enemy> EnemySpawned;

    private void Start()
    {
        for (var i = 0; i < _spawnSize; i++)
        {
            CreateNewEnemy();
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);

            var enemy = GetInactiveEnemy();

            if (enemy != null && !enemy.IsDie)
            {
                enemy.gameObject.SetActive(true);
            }
            else
            {
                var newEnemy = CreateNewEnemy();
                newEnemy.gameObject.SetActive(true);
            }
        }
    }

    private Enemy GetInactiveEnemy()
    {
        foreach (var enemy in _enemies)
        {
            if (!enemy.gameObject.activeSelf)
            {
                _enemies.Remove(enemy);
                return enemy;
            }
        }

        return null;
    }

    private Enemy CreateNewEnemy()
    {
        var newEnemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
        newEnemy.gameObject.SetActive(false);
        _enemies.Add(newEnemy);
        
        EnemySpawned?.Invoke(newEnemy);

        return newEnemy;
    }
}