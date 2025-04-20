using UnityEngine;

namespace SO
{
    [CreateAssetMenu( menuName = "Enemy", fileName = "New Enemy Data", order = 0)]
    public class EnemyUnitScriptableObject : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private float _attackDelay;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private int _rewardForKill;
        
        public int Health => _health;
        public float AttackDelay => _attackDelay;
        public int Damage => _damage;
        public float Speed => _speed;   
        public int RewardForKill => _rewardForKill;
    }
}