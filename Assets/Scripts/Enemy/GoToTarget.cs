using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class GoToTarget : Node
{
    private Transform _transform;

    private NavMeshAgent _agent;

    private LayerMask _playerMask;
    private LayerMask _wallMask;

    private float _counter = 0f;

    public GoToTarget(Transform transform, NavMeshAgent agent, LayerMask playerMask, LayerMask wallMask)
    {
        _transform = transform;
        _agent = agent;
        _playerMask = playerMask;
        _wallMask = wallMask;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        bool isHitPlayer = Physics.Linecast(_transform.position, target.position, _playerMask);
        bool isHitWall = Physics.Linecast(_transform.position, target.position, _wallMask);

        if (isHitPlayer && !isHitWall)
        {
            _agent.SetDestination(target.position);

        }
        else
        {
            _counter += Time.deltaTime;
            _agent.SetDestination(target.position);
            if(_counter >= 1f)
                ClearData("target");
        }

        state = NodeState.RUNNING;
        return state;
    }

}
