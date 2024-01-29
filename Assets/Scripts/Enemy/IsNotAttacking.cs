using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class IsNotAttacking : Node
{
    public override NodeState Evaluate()
    {
        if(!EnemyBT.attacking)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
