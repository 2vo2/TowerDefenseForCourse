using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;

    [SerializeField] private Transform _playerBasePoint;
    [SerializeField] private Transform _healthBarParent;
    [SerializeField] private Transform _healthBar;
    [SerializeField] private int _health;
    [SerializeField] private float _detectionRadius;

    private HealthBar _healthBarInstance;
    private EnemyDetector _enemyDetector;

    private EnemyUnit _currentEnemyUnit;
    
    public Transform PlayerBasePoint => _playerBasePoint;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this) 
            Destroy(this);

        _healthBarInstance = new HealthBar();
        _enemyDetector = new EnemyDetector();
        
        _healthBarInstance.LookAtCamera(_healthBarParent);
    }

    private void Update()
    {
        if (!_currentEnemyUnit)
        {
            _currentEnemyUnit = _enemyDetector.DetectEnemy(transform.position, _detectionRadius);
        }
        else
        {
            if (_currentEnemyUnit.IsAttack)
            {
                _healthBarInstance.ChangeHealthBar(_healthBar, _health);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
