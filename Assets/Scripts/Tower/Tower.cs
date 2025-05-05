using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerScriptableObject _towerData;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private TowerShooter _towerShooter;

    private EnemyDetector _enemyDetector;
    
    private EnemyUnit _currentEnemyUnit;
    private float _attackTimer;
    
    public TowerScriptableObject TowerData => _towerData;
    public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
    public TowerShooter TowerShooter => _towerShooter;
    
    private void Awake()
    {
        _towerShooter.OnAwake();
    }

    private void Update()
    {
        _towerShooter.Tick();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _towerData.ShootRadius);
    }
}