using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class IsDoorOpen : Node
{

    private Transform _transform;
    public GameObject door;
    private LayerMask _doorMask;

    public IsDoorOpen(Transform transform, LayerMask doorMask)
    {
        _transform = transform;
        _doorMask = doorMask;
    }

    public override NodeState Evaluate()
    {
        Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.4f, 0.4f, 0.4f), Quaternion.identity, _doorMask);
        if (hit[0].gameObject.GetComponent<DoorController>().doorOpen == true)
        {
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
