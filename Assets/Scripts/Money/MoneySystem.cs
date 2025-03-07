using System.Collections;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private GameInterface _gameUI;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _delay;

    private int _moneyValue;

    public int MoneyValue => _moneyValue;

    private void OnEnable()
    {
        foreach (var enemy in _enemySpawner.Enemies)
        {
            enemy.EnemyDied += OnEnemyDied;
        }

        _enemySpawner.EnemySpawned += OnNewEnemySpawned;
        
        print("Subscribed to Enemies and EnemySpawner");
    }

    private void Start()
    {
        StartCoroutine(AddMoneyPerSeconds());
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemySpawner.Enemies)
        {
            enemy.EnemyDied -= OnEnemyDied;
        }

        _enemySpawner.EnemySpawned -= OnNewEnemySpawned;
    }

    private void OnNewEnemySpawned(Enemy newEnemy)
    {
        newEnemy.EnemyDied += OnEnemyDied;
    }

    private IEnumerator AddMoneyPerSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);

            _moneyValue++;
            _gameUI.MoneyLabel.text = $"Money: {_moneyValue}";
        }
    }

    public void AddMoney(int addValue)
    {
        if (addValue <= 0) return;

        _moneyValue += addValue;
        _gameUI.MoneyLabel.text = $"Money: {_moneyValue}";
    }

    public void DeductMoney(int deductValue)
    {
        if (deductValue <= 0) return;

        _moneyValue -= deductValue;
        _gameUI.MoneyLabel.text = $"Money: {_moneyValue}";
    }

    private void OnEnemyDied(int rewardForKill)
    {
        AddMoney(rewardForKill);
    }
}