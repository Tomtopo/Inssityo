using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class PauseAfterHit : Node
{
    private Transform _transform;

    private float _counter = 0;

    public PauseAfterHit(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        _counter += Time.deltaTime;
        if(_counter >= 1f)
        {
            EnemyBT.attacking = false;
            _counter = 0f;
            Debug.Log("lataa");
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
