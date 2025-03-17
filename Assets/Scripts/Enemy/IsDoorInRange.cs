using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.InputSystem.HID;
using UnityEngine.AI;

public class IsDoorInRange : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private LayerMask _doorMask;

    public IsDoorInRange(Transform transform, NavMeshAgent agent, LayerMask doorMask)
    {
        _transform = transform;
        _agent = agent;
        _doorMask = doorMask;
    }

    public override NodeState Evaluate()
    {
        Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity, _doorMask);
        if(hit.Length == 1)
        {
            _agent.isStopped = true;
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
