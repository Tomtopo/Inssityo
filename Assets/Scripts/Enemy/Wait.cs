using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.AI;

public class Wait : Node
{
    private NavMeshAgent _agent;

    private float _waitTime;
    private float _counter = 0f;

    public Wait(float waitTime, NavMeshAgent agent)
    {
        _waitTime = waitTime;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        EnemyBT.isWaiting = true;
        _counter += Time.deltaTime;
        if(_counter >= _waitTime)
        {
            EnemyBT.isWaiting = false;
            _agent.isStopped = false;
            _counter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
