using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using UnityEngine.AI;

public class EnemyBT : Tree
{
    public UnityEngine.Transform waypointParent;
    public List<UnityEngine.Transform> waypoints = new List<UnityEngine.Transform>();
    public UnityEngine.GameObject player;
    public NavMeshAgent agent;

    public UnityEngine.LayerMask playerMask;
    public UnityEngine.LayerMask wallMask;
    public UnityEngine.LayerMask waypointsMask;

    public static bool attacking = false;
    public static bool isWaiting = false;
    public static bool isLookingLeftAndRight = false;

    public static float speed = 4f;
    public static float sightReach = 10f;
    public static float waypointReach = 30f;
    public static float attackRange = 2f;
    public static int randomWaypointIndex = 0;
    public static UnityEngine.Transform lastWaypointVisited;
    public static UnityEngine.Transform currentWaypoint;
    public static List<UnityEngine.Transform> possibleWaypoints = new List<UnityEngine.Transform>();
    public static List<UnityEngine.Transform> visibleWaypoints = new List<UnityEngine.Transform>();

    protected override Node SetupTree()
    {
        foreach(UnityEngine.Transform child in waypointParent)
        {
            waypoints.Add(child);
        }
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckTargetInAttackRange(transform, agent),
                new HitTarget(transform, player, player.GetComponent<PlayerHealth>(), playerMask),
            }),
            new Sequence(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new CheckTargetInFOVRange(transform, player, playerMask, wallMask),
                    new CheckIfTargetMakingNoise(transform, player, agent, playerMask, wallMask),
                }),
                new GoToTarget(transform, agent, playerMask, wallMask),
            }),
            new Sequence(new List<Node>
            {
                new GoToRandomWaypoint(transform, agent, waypoints),
                new LookLeftAndRight(transform),
            }),

        });

        return root;
    }
}
