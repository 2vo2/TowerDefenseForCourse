using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyUnit _enemy;
    [SerializeField] private Animator _animator;

    private readonly int DieAnimation = Animator.StringToHash("die");
    private readonly int AttackAnimation = Animator.StringToHash("attack");
    private readonly int HitAnimation = Animator.StringToHash("hit");
    
    private void OnEnable()
    {
        _enemy.EnemyDied += OnEnemyDied;
        _enemy.EnemyAttack += OnEnemyAttack;
        //_enemy.EnemyHitted += OnEnemyHitted;
    }

    private void OnDisable()
    {
        _enemy.EnemyDied -= OnEnemyDied;
        _enemy.EnemyAttack -= OnEnemyAttack;
        //_enemy.EnemyHitted -= OnEnemyHitted;
    }

    private void OnEnemyDied(int value)
    {
        PlayAnimation(DieAnimation);
    }

    private void OnEnemyAttack()
    {
        _animator.SetTrigger(AttackAnimation);
    }

    // private void OnEnemyHitted()
    // {
    //     _animator.SetLayerWeight(1, 1f);
    //     _animator.SetTrigger(HitAnimation);
    // }

    private void PlayAnimation(int stateHashName)
    {
        _animator.CrossFade(stateHashName, 0.1f);
    }
    
    public void OnDiedAnimationFinished()
    {
        _enemy.gameObject.SetActive(false);
    }

    // public void OnHitAnimationFinished()
    // {
    //     _animator.SetLayerWeight(0, 0f);
    // }
}