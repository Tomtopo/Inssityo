using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class Wait : Node
{
    private float _waitTime;
    private float _counter = 0f;

    public Wait(float waitTime)
    {
        _waitTime = waitTime;
    }

    public override NodeState Evaluate()
    {
        _counter += Time.deltaTime;
        if(_counter >= 1f)
        {
            _counter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
