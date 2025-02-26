using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _pointToGo;
    [SerializeField] private float _speed;

    private void Start()
    {
        _navMeshAgent.speed = _speed;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_pointToGo.position);
    }
}
