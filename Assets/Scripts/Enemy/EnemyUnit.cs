using System.Collections;
using SO;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] private EnemyUnitScriptableObject _enemyData;
    [SerializeField] private Transform _pointToHit;
    [SerializeField] private Transform _healthBarParent;
    [SerializeField] private Transform _healthBar;
    
    private int _maxHealth;
    private int _currentHealth;
    private HealthBar _healthBarInstance;
    private bool _isDie;

    public event UnityAction<int> EnemyDied;
    
    public bool IsDie => _isDie;
    public Transform PointToHit => _pointToHit;

    private void Awake()
    {
        _healthBarInstance = new HealthBar();
        
        _maxHealth = _enemyData.Health;
        _currentHealth = _maxHealth;
    }
    
    private void Update()
    {
        _healthBarInstance.LookAtCamera(_healthBarParent);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            _currentHealth -= projectile.Damage;

            _healthBarInstance.ChangeHealthBar(_healthBar, _currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase playerBase))
        {
            if (playerBase.gameObject.activeSelf)
            {
                StartCoroutine(Attack(playerBase));
            }
            else
            {
                StopCoroutine(Attack(playerBase));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase playerBase))
        {
            StopCoroutine(Attack(playerBase));
        }
    }

    private IEnumerator Attack(PlayerBase playerBase)
    {
        while (true)
        {
            playerBase.TakeDamage(_enemyData.Damage);
            yield return new WaitForSeconds(_enemyData.AttackDelay);
        }
    }
    
    private void Die()
    {
        _isDie = true;
        EnemyDied?.Invoke(_enemyData.RewardForKill);
        
        StopAllCoroutines();
        
        gameObject.SetActive(false);
    }
}
