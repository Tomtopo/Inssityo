using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class CheckTargetInAttackRange : Node
{
    private Transform _transform;

    private NavMeshAgent _agent;

    public CheckTargetInAttackRange(Transform transform, NavMeshAgent agent)
    { 
        _transform = transform;
        _agent = agent;
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
            _agent.ResetPath();
            EnemyBT.attacking = true;
            EnemyBT.attackRange = Mathf.Infinity;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }


}
