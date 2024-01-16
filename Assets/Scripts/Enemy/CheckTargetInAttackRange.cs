using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckTargetInAttackRange : Node
{
    private Transform _transform;

    public CheckTargetInAttackRange(Transform transform)
    { 
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
        if(Vector3.Distance(_transform.position, target.position) < EnemyBT.attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }


}
