using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BargeDoor : Node
{
    private Transform _transform;
    private NavMeshAgent _agent;
    private LayerMask _doorMask;
    private float _bargePower;

    public BargeDoor(Transform transform, NavMeshAgent agent, LayerMask doorMask, float bargePower)
    {
        _transform = transform;
        _agent = agent;
        _doorMask = doorMask;
        _bargePower = bargePower;
    }

    public override NodeState Evaluate()
    {
        Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity, _doorMask);
        hit[0].gameObject.GetComponent<DoorController>().BargeDoor(_transform.position, _bargePower);
        
        state = NodeState.RUNNING;
        return state;

    }
}
