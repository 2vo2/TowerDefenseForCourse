using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private GameInterface _gameUI;
    [FormerlySerializedAs("_enemySpawner")] [SerializeField] private EnemyBase _enemyBase;
    [SerializeField] private float _delay;

    private int _moneyValue;

    public int MoneyValue => _moneyValue;

    private void OnEnable()
    {
        foreach (var enemy in _enemyBase.Enemies)
        {
            enemy.EnemyDied += OnEnemyDied;
        }

        _enemyBase.EnemySpawned += OnNewEnemySpawned;
    }

    private void Start()
    {
        StartCoroutine(AddMoneyPerSeconds());
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemyBase.Enemies)
        {
            enemy.EnemyDied -= OnEnemyDied;
        }

        _enemyBase.EnemySpawned -= OnNewEnemySpawned;
    }

    private void OnNewEnemySpawned(EnemyUnit newEnemyUnit)
    {
        newEnemyUnit.EnemyDied += OnEnemyDied;
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