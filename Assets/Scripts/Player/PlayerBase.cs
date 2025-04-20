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
    private int _currentHealth;
    
    public Transform PlayerBasePoint => _playerBasePoint;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this) 
            Destroy(this);

        _healthBarInstance = new HealthBar();
        
        _healthBarInstance.LookAtCamera(_healthBarParent);
        
        _currentHealth = _health;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
