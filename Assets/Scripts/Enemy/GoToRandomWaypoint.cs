using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class GoToRandomWaypoint : Node
{
    private Transform _transform;
    private List<Transform> _visibleWaypoints;
    private List<Transform> _waypoints;

    private NavMeshAgent _agent;
    public GoToRandomWaypoint(Transform transform, NavMeshAgent agent, List<Transform> waypoints)
    {
        _transform = transform;
        _agent = agent;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (_agent.remainingDistance < 0.1f && _agent.hasPath)
        {
            _agent.ResetPath();
        }

        if (!_agent.hasPath)
        {
            int destinationIndex = Random.Range(0, _waypoints.Count);
            _agent.SetDestination(_waypoints[destinationIndex].position);
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
