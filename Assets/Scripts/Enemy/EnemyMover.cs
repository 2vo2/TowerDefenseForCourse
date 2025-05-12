using SO;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyUnit _enemyUnit;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Transform _pointToGo;
    
    private void Start()
    {
        _pointToGo = PlayerBase.Instance.PlayerBasePoint;
        _navMeshAgent.speed = _enemyUnit.EnemyData.Speed;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_pointToGo.position);
    }
}
