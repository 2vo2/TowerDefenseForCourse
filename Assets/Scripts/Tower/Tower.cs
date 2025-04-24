using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerScriptableObject _towerData;
    [SerializeField] private Transform _projectileSpawnPoint;
    
    private EnemyDetector _enemyDetector;
    private TowerShooter _towerShooter;

    private float _shootRadius;
    private float _shootSpeed;
    private float _shootForce;
    private Projectile _projectile;
    private int _cost;
    
    private EnemyUnit _currentEnemyUnit;
    private float _attackTimer;
    
    public TowerScriptableObject TowerData => _towerData;
    
    private void Awake()
    {
        _shootRadius = _towerData.ShootRadius;
        _shootSpeed = _towerData.ShootSpeed;
        _shootForce = _towerData.ShootForce;
        _projectile = _towerData.Projectile;
        _cost = _towerData.Cost;

        _enemyDetector = new EnemyDetector();
        _towerShooter = new TowerShooter();
    }
    
    private void Update()
    {
        _attackTimer += Time.deltaTime;
    
        if (!_currentEnemyUnit && TowerPlacer.Instance.IsTowerPlaced)
        {
            _currentEnemyUnit = _enemyDetector.DetectEnemy(transform.position, _shootRadius);
        }
        
        if (_currentEnemyUnit && _currentEnemyUnit.IsDie)
        {
            _currentEnemyUnit = null;
            return;
        }

        if (_attackTimer >= _shootSpeed && _currentEnemyUnit)
        {
            Shoot();
            _attackTimer = 0;

            if (!_enemyDetector.IsEnemyInRange(transform.position, _currentEnemyUnit, _shootRadius))
            {
                _currentEnemyUnit = null;
            }
        }
    }

    
    private void Shoot()
    {
        var newProjectile = Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity, transform);
        var shootDirection = _currentEnemyUnit.PointToHit.transform.position - _projectileSpawnPoint.position;
        newProjectile.transform.LookAt(_currentEnemyUnit.transform);

        _towerShooter.Shoot(newProjectile, shootDirection, _shootForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootRadius);
    }
}