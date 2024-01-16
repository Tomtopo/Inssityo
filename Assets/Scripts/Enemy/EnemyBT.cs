using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

public class EnemyBT : Tree
{
    public UnityEngine.Transform[] waypoints;
    public UnityEngine.GameObject player;

    public UnityEngine.LayerMask playerMask;
    public UnityEngine.LayerMask wallMask;
    public UnityEngine.LayerMask waypointsMask;

    public static float speed = 4f;
    public static float reach = 10f;
    public static float waypointReach = 20f;
    public static float attackRange = 2f;
    public static int randomWaypointIndex = 0;
    public static List<UnityEngine.Transform> possibleWaypoints = new List<UnityEngine.Transform>();
    public static List<UnityEngine.Transform> visibleWaypoints = new List<UnityEngine.Transform>();

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckTargetInAttackRange(transform),
                new HitTarget(transform, player, player.GetComponent<PlayerHealth>()),
            }),
            new Sequence(new List<Node>
            {
                new CheckTargetInFOVRange(transform, player, playerMask, wallMask),
                new GoToTarget(transform, playerMask, wallMask),
            }),
            new Sequence(new List<Node>
            {
                new CheckWaypointsInRange(transform, waypoints),
                new PickVisibleWaypoints(transform, possibleWaypoints, waypointsMask, wallMask),
                new GoToRandomWaypoint(transform, visibleWaypoints),
            }),
        }) ;

        return root;
    }
}
