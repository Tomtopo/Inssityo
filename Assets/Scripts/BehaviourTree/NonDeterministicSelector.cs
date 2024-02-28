using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace BehaviourTree
{
    public class NonDeterministicSelector : Node
    {
        public NonDeterministicSelector() : base() { }
        public NonDeterministicSelector(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
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

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}


