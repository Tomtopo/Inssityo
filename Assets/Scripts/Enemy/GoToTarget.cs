using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GoToTarget : Node
{
    private Transform _transform;

    private LayerMask _playerMask;
    private LayerMask _wallMask;

    public GoToTarget(Transform transform, LayerMask playerMask, LayerMask wallMask)
    {
        _transform = transform;
        _playerMask = playerMask;
        _wallMask = wallMask;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        bool isHitPlayer = Physics.Linecast(_transform.position, target.position, _playerMask);
        bool isHitWall = Physics.Linecast(_transform.position, target.position, _wallMask);

        if (Vector3.Distance(_transform.position, target.position) < EnemyBT.reach && isHitPlayer && !isHitWall)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, EnemyBT.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }
        else
        {
            ClearData("target");
        }

        state = NodeState.RUNNING;
        return state;
    }

}
