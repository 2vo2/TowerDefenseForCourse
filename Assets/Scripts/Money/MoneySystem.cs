using System;
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
        UpdateMoney(addValue, true);
    }

    public void DeductMoney(int deductValue)
    {
        UpdateMoney(deductValue, false);
    }

    private void UpdateMoney(int value, bool isAddition)
    {
        if (value <= 0) return;

        _moneyValue = isAddition ? _moneyValue + value : _moneyValue - value;
        _gameUI.MoneyLabel.text = $"Money: {_moneyValue}";
    }

    private void OnEnemyDied(int rewardForKill)
    {
        AddMoney(rewardForKill);
    }
}