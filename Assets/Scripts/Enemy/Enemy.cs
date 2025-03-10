using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _pointToHit;
    [SerializeField] private Transform _healthBarParent;
    [SerializeField] private Transform _healthBar;
    [SerializeField] private int _rewardForKill;
    
    private HealthBar _healthBarInstance;
    private bool _isDie;

    public event UnityAction<int> EnemyDied;
    
    public bool IsDie => _isDie;
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
                _isDie = true;
                EnemyDied?.Invoke(_rewardForKill);
                gameObject.SetActive(false);
            }
        }
    }

}
