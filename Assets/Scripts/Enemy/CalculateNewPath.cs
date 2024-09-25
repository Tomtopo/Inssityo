using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CalculateNewPath : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private List<Transform> _waypoints;

    public CalculateNewPath(Transform transform, NavMeshAgent agent, List<Transform> waypoints)
    {
        _transform = transform;
        _agent = agent;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        int destinationIndex = Random.Range(0, _waypoints.Count);
        _agent.SetDestination(_waypoints[destinationIndex].position);

        state = NodeState.SUCCESS;
        return state;

    }
}
