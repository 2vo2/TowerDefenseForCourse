using System;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [Serializable]
    public class LevelWaveSettings
    {
        public EnemyUnit EnemyType;
        public int EnemiesCount;
        public float EnemySpawnDelay;
        public float PauseAfterWave;
    }
    
    [CreateAssetMenu(fileName = "New Level Waves Data", menuName = "Level Waves", order = 0)]
    public class LevelWavesScriptableObject : ScriptableObject
    {
        [SerializeField] private List<LevelWaveSettings> _waves;
        
        public List<LevelWaveSettings> Waves => _waves;
    }
}