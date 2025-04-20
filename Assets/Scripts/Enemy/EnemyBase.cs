using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private LevelWavesScriptableObject _levelWavesData;
    [SerializeField] private EnemyUnit _enemyUnitPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _spawnSize;
    [SerializeField] private float _spawnDelay;

    private List<EnemyUnit> _enemies = new List<EnemyUnit>();

    public List<EnemyUnit> Enemies => _enemies;
    public event UnityAction<EnemyUnit> EnemySpawned;

    private void Start()
    {
        for (var i = 0; i < _levelWavesData.Waves.Count; i++)
        {
            for (var j = 0; j < _spawnSize; j++)
            {
                
            }
        }

        //StartCoroutine(SpawnEnemies());
    }

    private EnemyUnit CreateNewEnemy(EnemyUnit enemyUnit)
    {
        var newEnemy = Instantiate(enemyUnit, _spawnPoint.position, _spawnPoint.rotation, transform);
        newEnemy.gameObject.SetActive(false);
        _enemies.Add(newEnemy);
        
        EnemySpawned?.Invoke(newEnemy);

        return newEnemy;
    }

    // private IEnumerator SpawnEnemies()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(_spawnDelay);
    //
    //         var enemy = GetInactiveEnemy();
    //
    //         if (enemy != null && !enemy.IsDie)
    //         {
    //             enemy.gameObject.SetActive(true);
    //         }
    //         else
    //         {
    //             var newEnemy = CreateNewEnemy();
    //             newEnemy.gameObject.SetActive(true);
    //         }
    //     }
    // }

    private EnemyUnit GetInactiveEnemy()
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
}