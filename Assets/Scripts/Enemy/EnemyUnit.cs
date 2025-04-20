using System;
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
    
    private int _health;
    private HealthBar _healthBarInstance;
    private bool _isDie;

    public event UnityAction<int> EnemyDied;
    
    public bool IsDie => _isDie;
    public Transform PointToHit => _pointToHit;

    private void Awake()
    {
        _healthBarInstance = new HealthBar();
        
        _health = _enemyData.Health;
    }
    
    private void Update()
    {
        _healthBarInstance.LookAtCamera(_healthBarParent);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            _health--;

            _healthBarInstance.ChangeHealthBar(_healthBar, _health);

            if (_health <= 0)
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
