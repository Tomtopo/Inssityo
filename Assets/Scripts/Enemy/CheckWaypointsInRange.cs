using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckWaypointsInRange : Node
{

    private Transform _transform;
    private Transform[] _waypoints;

    public CheckWaypointsInRange(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(EnemyBT.possibleWaypoints.Count == 0 || t == null)
        {
            EnemyBT.possibleWaypoints.Clear();
            for (int i = 0; i < _waypoints.Length; i++)
            {
                if (Vector3.Distance(_transform.position, _waypoints[i].position) < EnemyBT.waypointReach)
                {
                    EnemyBT.possibleWaypoints.Add(_waypoints[i]);
                }
            }
        }

        if(EnemyBT.possibleWaypoints.Count > 0)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;

    }
}
