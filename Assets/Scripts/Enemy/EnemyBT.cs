using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.GameObject player;

    public UnityEngine.LayerMask playerMask;
    public UnityEngine.LayerMask wallMask;

    public static float speed = 4f;
    public static float reach = 10f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(transform, player, playerMask, wallMask),
                new GoToTarget(transform, playerMask, wallMask),
            }),
            new Patrol(transform, waypoints),
        });

        return root;
    }
}
