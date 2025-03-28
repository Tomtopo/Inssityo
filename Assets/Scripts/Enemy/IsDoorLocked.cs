using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsDoorLocked : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private LayerMask _doorMask;

    public IsDoorLocked(Transform transform, NavMeshAgent agent, LayerMask doorMask)
    {
        _transform = transform;
        _agent = agent;
        _doorMask = doorMask;
    }

    public override NodeState Evaluate()
    {
        Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity, _doorMask);
        if (hit[0].gameObject.GetComponent<DoorController>().isDoorLocked)
        {
            _agent.isStopped = true;
            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            state = NodeState.SUCCESS;
            return state;
        }
    }
}
