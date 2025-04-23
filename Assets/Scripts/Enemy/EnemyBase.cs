using System.Collections;
using System.Collections.Generic;
using SO;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private LevelWavesScriptableObject _levelWavesData;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _spawnSize;
    
    private List<GameObject> _wavesParent = new List<GameObject>();
    private Dictionary<int, List<EnemyUnit>> _waveEnemies = new Dictionary<int, List<EnemyUnit>>();

    public Dictionary<int, List<EnemyUnit>> WaveEnemies => _waveEnemies;
    public event UnityAction<EnemyUnit> EnemySpawned;
    public event UnityAction<int, int> WaveActivated;
    public event UnityAction<int> EnemyLeft; 
    public event UnityAction<float, bool> PauseAfterWave;
    public event UnityAction<string> WavesEnded;

    private void Start()
    {
        for (var i = 0; i < _levelWavesData.Waves.Count; i++)
        {
            var newWaveParent = SpawnWaveParent(i);
            _wavesParent.Add(newWaveParent);
            
            for (var j = 0; j < _levelWavesData.Waves[i].EnemiesCount; j++)
            {
                CreateNewEnemy(_levelWavesData.Waves[i].EnemyType, newWaveParent.transform, i);
            }
        }
        
        StartCoroutine(ActivatingEnemies());
    }

    private GameObject SpawnWaveParent(int i)
    {
        var waveParent = new GameObject();
        waveParent.transform.name = $"EnemiesFromWave_{i + 1}";
        waveParent.transform.parent = transform;
        return waveParent;
    }

    private EnemyUnit CreateNewEnemy(EnemyUnit enemyUnit, Transform parent, int waveIndex)
    {
        var newEnemy = Instantiate(enemyUnit, _spawnPoint.position, _spawnPoint.rotation, parent);
        newEnemy.gameObject.SetActive(false);
        
        if (!_waveEnemies.ContainsKey(waveIndex))
            _waveEnemies[waveIndex] = new List<EnemyUnit>();
        
        _waveEnemies[waveIndex].Add(newEnemy);
        
        EnemySpawned?.Invoke(newEnemy);

        return newEnemy;
    }

    private IEnumerator ActivatingEnemies()
    {
        for (var i = 0; i < _levelWavesData.Waves.Count; i++)
        {
            var enemiesCount = _levelWavesData.Waves[i].EnemiesCount;
            var enemyType = _levelWavesData.Waves[i].EnemyType;
            var enemySpawnDelay = _levelWavesData.Waves[i].EnemySpawnDelay;
            var pauseAfterWave = _levelWavesData.Waves[i].PauseAfterWave;
            var parent = _wavesParent[i].transform;

            var spawned = 0;
            var spawnTimer = 0f;
            
            WaveActivated?.Invoke(i, _levelWavesData.Waves.Count);
            EnemyLeft?.Invoke(enemiesCount);

            while (spawned < enemiesCount)
            {
                spawnTimer += Time.deltaTime;
                
                if (spawnTimer >= enemySpawnDelay)
                {
                    spawnTimer = 0f;

                    var enemy = GetInactiveEnemy(i);

                    if (enemy != null && !enemy.IsDie)
                    {
                        enemy.gameObject.SetActive(true);
                    }
                    else
                    {
                        var newEnemy = CreateNewEnemy(enemyType, parent, i);
                        newEnemy.gameObject.SetActive(true);
                    }
                    
                    spawned++;
                    
                    var enemiesLeft = Mathf.Max(0, enemiesCount - spawned);
                    EnemyLeft?.Invoke(enemiesLeft);
                }

                yield return null;
            }
            
            var pauseElapsed = 0f;

            while (pauseElapsed < pauseAfterWave)
            {
                pauseElapsed += Time.deltaTime;
                var pauseTimeLeft = Mathf.Max(0f, pauseAfterWave - pauseElapsed);

                PauseAfterWave?.Invoke(pauseTimeLeft, true);
                
                yield return null;
                
                PauseAfterWave?.Invoke(0f, false);
            }
        }
        
        WavesEnded?.Invoke("WIN!");
    }

    private EnemyUnit GetInactiveEnemy(int waveIndex)
    {
        if (!_waveEnemies.ContainsKey(waveIndex))
            return null;

        var waveList = _waveEnemies[waveIndex];

        foreach (var enemy in waveList)
        {
            if (!enemy.gameObject.activeSelf)
            {
                return enemy;
            }
        }

        return null;
    }

}