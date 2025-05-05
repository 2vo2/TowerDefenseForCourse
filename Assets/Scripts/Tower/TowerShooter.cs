using UnityEngine;
using UnityEngine.Events;

public class TowerShooter : MonoBehaviour, IAwakeable, ITickable
{
    [SerializeField] private Tower _tower;
    
    private EnemyDetector _enemyDetector;
    private EnemyUnit _currentEnemyUnit;
    private float _attackTimer;

    public event UnityAction TowerShooted;

    public void OnAwake()
    {
        _enemyDetector = new EnemyDetector();
    }
    
    public void Tick()
    {
        _attackTimer += Time.deltaTime;
    
        if (!_currentEnemyUnit && TowerPlacer.Instance.IsTowerPlaced)
        {
            _currentEnemyUnit = _enemyDetector.DetectEnemy(transform.position, _tower.TowerData.ShootRadius);
        }
        
        if (_currentEnemyUnit && _currentEnemyUnit.IsDie)
        {
            _currentEnemyUnit = null;
            return;
        }

        if (_attackTimer >= _tower.TowerData.ShootSpeed && _currentEnemyUnit)
        {
            Shoot();
            TowerShooted?.Invoke();
            _attackTimer = 0;

            if (!_enemyDetector.IsEnemyInRange(transform.position, _currentEnemyUnit, _tower.TowerData.ShootRadius))
            {
                _currentEnemyUnit = null;
            }
        }

    }
    
    private void Shoot()
    {
        var newProjectile = Instantiate(_tower.TowerData.Projectile, _tower.ProjectileSpawnPoint.position, Quaternion.identity, transform);
        var shootDirection = _currentEnemyUnit.PointToHit.transform.position - _tower.ProjectileSpawnPoint.position;
        newProjectile.transform.LookAt(_currentEnemyUnit.transform);

        AddForceToProjectile(newProjectile, shootDirection, _tower.TowerData.ShootForce);
    }
    
    private void AddForceToProjectile(Projectile projectile, Vector3 shootDirection, float shootForce)
    {
        projectile.Rigidbody.AddForce(shootDirection * shootForce, ForceMode.Acceleration);
    }
}
