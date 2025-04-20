using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Transform _pointToHit;
    [SerializeField] private Transform _healthBarParent;
    [SerializeField] private Transform _healthBar;
    [SerializeField] private int _rewardForKill;
    
    private HealthBar _healthBarInstance;
    private bool _isDie;
    private bool _isAttack;

    public event UnityAction<int> EnemyDied;
    
    public bool IsDie => _isDie;
    public bool IsAttack => _isAttack;
    public Transform PointToHit => _pointToHit;

    private void Awake()
    {
        _healthBarInstance = new HealthBar();
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
            if (!_isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerBase playerBase))
        {
            StopCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            _isAttack = true;
        
            yield return new WaitForSeconds(_attackDelay);
            
            _isAttack = false;   
        }
    }
    
    private void Die()
    {
        _isDie = true;
        EnemyDied?.Invoke(_rewardForKill);
        
        StopCoroutine(Attack());
        
        gameObject.SetActive(false);
    }
}
