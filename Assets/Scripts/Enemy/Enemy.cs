using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _uiParent;
    [SerializeField] private Transform _healthBar;

    private EnemyUI _enemyUI;
    private bool _isDie;

    public bool IsDie => _isDie;

    private void Awake()
    {
        _enemyUI = new EnemyUI();
    }

    private void Update()
    {
        _enemyUI.LookAtCamera(_uiParent);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            _health--;

            _enemyUI.ChangeHealthBar(_healthBar, _health);

            if (_health <= 0)
            {
                _isDie = true;
                gameObject.SetActive(false);
            }
        }
    }

}
