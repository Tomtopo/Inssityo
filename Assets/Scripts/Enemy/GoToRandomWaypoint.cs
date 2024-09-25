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

    private bool _waiting = false;
    private float _counter = 0f;
    private float _waitTime = 2f;


    public GoToRandomWaypoint(Transform transform, NavMeshAgent agent, List<Transform> waypoints)
    {
        _transform = transform;
        _agent = agent;
        _waypoints = waypoints;
    }

    //public override NodeState Evaluate()
    //{
    //    Transform target = (Transform)GetData("target");
    //    //_agent.updatePosition = true;
    //    //_transform.position = _agent.nextPosition;
    //    //_agent.updatePosition = false;
    //    if (_agent.remainingDistance < 0.1f && _agent.hasPath)
    //    {
    //        EnemyBT.isLookingLeftAndRight = true;
    //    }
    //    if (EnemyBT.isLookingLeftAndRight)
    //    {
    //        state = NodeState.SUCCESS;
    //        return state;
    //    }
    //    if (!_agent.hasPath && !EnemyBT.isLookingLeftAndRight)
    //    {
    //        //_agent.updatePosition = false;
    //        int destinationIndex = Random.Range(0, _waypoints.Count);
    //        _agent.SetDestination(_waypoints[destinationIndex].position);
    //    }

    //    state = NodeState.FAILURE;
    //    return state;
    //}

    public override NodeState Evaluate()
    {
        Debug.Log(_agent.hasPath);
        Transform target = (Transform)GetData("target");
        //_agent.updatePosition = true;
        //_transform.position = _agent.nextPosition;
        //_agent.updatePosition = false;
        if (_agent.remainingDistance < 0.1f && _agent.hasPath)
        {
            _agent.ResetPath();
        }

        if (!_agent.hasPath /*&& !EnemyBT.isLookingLeftAndRight*/)
        {
            //_agent.updatePosition = false;
            int destinationIndex = Random.Range(0, _waypoints.Count);
            _agent.SetDestination(_waypoints[destinationIndex].position);
        }

        state = NodeState.SUCCESS;
        return state;
    }



    //public GoToRandomWaypoint(Transform transform, List<Transform> visibleWaypoints)
    //{
    //    _transform = transform;
    //    _visibleWaypoints = visibleWaypoints;
    //}

    //public override NodeState Evaluate()
    //{
    //    if (Vector3.Distance(_transform.position, _visibleWaypoints[EnemyBT.randomWaypointIndex].position) > 0.1f)
    //    {
    //        _transform.position = Vector3.MoveTowards(_transform.position, _visibleWaypoints[EnemyBT.randomWaypointIndex].position, EnemyBT.speed * Time.deltaTime);
    //        Vector3 targetDirection = _visibleWaypoints[EnemyBT.randomWaypointIndex].position - _transform.position;
    //        float singleStep = EnemyBT.speed * Time.deltaTime;
    //        Vector3 newDirection = Vector3.RotateTowards(_transform.forward, targetDirection, singleStep, 0.0f);
    //        _transform.rotation = Quaternion.LookRotation(newDirection);
    //    }
    //    else
    //    {
    //        EnemyBT.lastWaypointVisited = EnemyBT.currentWaypoint;
    //        EnemyBT.currentWaypoint = EnemyBT.visibleWaypoints[EnemyBT.randomWaypointIndex];
    //        EnemyBT.possibleWaypoints.Clear();
    //        EnemyBT.visibleWaypoints.Clear();
    //    }

    //    state = NodeState.RUNNING;
    //    return state;
    //}
}
