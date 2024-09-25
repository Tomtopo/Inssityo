using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenDoor : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private LayerMask _doorMask;

    public OpenDoor(Transform transform, NavMeshAgent agent, LayerMask doorMask)
    {
        _transform = transform;
        _agent = agent;
        _doorMask = doorMask;
    }

    public override NodeState Evaluate()
    {
        Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.8f, 0.8f, 0.8f), Quaternion.identity, _doorMask);
        hit[0].gameObject.GetComponent<DoorController>().InteractWithDoor();

        state = NodeState.SUCCESS;
        return state;

    }
}
