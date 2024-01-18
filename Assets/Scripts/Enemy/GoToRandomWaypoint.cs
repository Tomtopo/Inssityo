using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class GoToRandomWaypoint : Node
{
    private Transform _transform;
    private List<Transform> _visibleWaypoints;

    public GoToRandomWaypoint(Transform transform, List<Transform> visibleWaypoints)
    {
        _transform = transform;
        _visibleWaypoints = visibleWaypoints;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_transform.position, _visibleWaypoints[EnemyBT.randomWaypointIndex].position) > 0.1f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _visibleWaypoints[EnemyBT.randomWaypointIndex].position, EnemyBT.speed * Time.deltaTime);
            Vector3 targetDirection = _visibleWaypoints[EnemyBT.randomWaypointIndex].position - _transform.position;
            float singleStep = EnemyBT.speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, targetDirection, singleStep, 0.0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            EnemyBT.lastWaypointVisited = EnemyBT.currentWaypoint;
            EnemyBT.currentWaypoint = EnemyBT.visibleWaypoints[EnemyBT.randomWaypointIndex];
            EnemyBT.possibleWaypoints.Clear();
            EnemyBT.visibleWaypoints.Clear();
        }
      
        state = NodeState.RUNNING;
        return state;
    }
}
