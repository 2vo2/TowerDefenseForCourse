using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _speed;

    private Transform _pointToGo;
    
    private void Start()
    {
        _pointToGo = PlayerBase.Instance.PlayerBasePoint;
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_pointToGo.position);
    }
}
