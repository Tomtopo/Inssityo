using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 4f;

    protected override Node SetupTree()
    {
        Node root = new Patrol(transform, waypoints);

        return root;
    }
}
