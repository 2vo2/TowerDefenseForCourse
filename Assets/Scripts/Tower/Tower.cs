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
    
    private Enemy _currentEnemy;
    private float _attackTimer;
    
    private void Awake()
    {
        _shootRadius = _towerData.ShootRadius;
        _shootSpeed = _towerData.ShootSpeed;
        _shootForce = _towerData.ShootForce;
        _projectile = _towerData.Projectile;

        _enemyDetector = new EnemyDetector();
        _towerShooter = new TowerShooter();
    }
    
    private void Update()
    {
        _attackTimer += Time.deltaTime;
    
        if (!_currentEnemy && TowerPlacer.Instance.IsTowerPlaced)
        {
            _currentEnemy = _enemyDetector.DetectEnemy(transform.position, _shootRadius);
        }
        
        if (_currentEnemy && _currentEnemy.IsDie)
        {
            _currentEnemy = null;
            return;
        }

        if (_attackTimer >= _shootSpeed && _currentEnemy)
        {
            Shoot();
            _attackTimer = 0;

            if (!_enemyDetector.IsEnemyInRange(transform.position, _currentEnemy, _shootRadius))
            {
                _currentEnemy = null;
            }
        }
    }

    
    private void Shoot()
    {
        var newProjectile = Instantiate(_projectile, _projectileSpawnPoint.position, Quaternion.identity, transform);
        var shootDirection = _currentEnemy.PointToHit.transform.position - _projectileSpawnPoint.position;

        _towerShooter.Shoot(newProjectile, shootDirection, _shootForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _shootRadius);
    }
}