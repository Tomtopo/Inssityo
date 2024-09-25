using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveIntoRoom : Node
{

    private NavMeshAgent _agent;

    public MoveIntoRoom(NavMeshAgent agent)
    {
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        _agent.isStopped = false;
        state = NodeState.SUCCESS;
        return state;
    }
}
