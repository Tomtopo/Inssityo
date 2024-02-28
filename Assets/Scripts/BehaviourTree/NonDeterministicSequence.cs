using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;


namespace BehaviourTree
{
    public class NonDeterministicSequence : Node
    {
        public NonDeterministicSequence() : base() { }
        public NonDeterministicSequence(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            List<Node> shuffled = children;

            var count = shuffled.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = shuffled[i];
                shuffled[i] = shuffled[r];
                shuffled[r] = tmp;
            }

            foreach (Node node in shuffled)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }


    }
}

