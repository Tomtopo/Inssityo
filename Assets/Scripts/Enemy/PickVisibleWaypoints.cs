using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class PickVisibleWaypoints : Node
{
    private Transform _transform;
    private List<Transform> _possibleWaypoints;

    private LayerMask _wallMask;
    private LayerMask _waypointMask;

    public PickVisibleWaypoints(Transform transform, List<Transform> possibleWaypoints, LayerMask waypointMask, LayerMask wallMask)
    {
        _transform = transform;
        _possibleWaypoints = possibleWaypoints;
        _waypointMask = waypointMask;
        _wallMask = wallMask;
    }

    public override NodeState Evaluate()
    {
        if(EnemyBT.visibleWaypoints.Count == 0)
        {
            for (int i = 0; i < _possibleWaypoints.Count; i++)
            {
                bool isHitWaypoint = Physics.Linecast(_transform.position, _possibleWaypoints[i].position, _waypointMask);
                bool isHitWall = Physics.Linecast(_transform.position, _possibleWaypoints[i].position, _wallMask);
                if (isHitWaypoint && !isHitWall)
                {
                    EnemyBT.visibleWaypoints.Add(_possibleWaypoints[i]);
                }
            }
            EnemyBT.randomWaypointIndex = Random.Range(0, EnemyBT.visibleWaypoints.Count);
        }

        if (EnemyBT.visibleWaypoints.Count > 0)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
