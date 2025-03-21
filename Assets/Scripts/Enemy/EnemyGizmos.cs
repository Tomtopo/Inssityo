using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

using BehaviourTree;

public class EnemyGizmos : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _wallMask;

    private float _fovRange = 90f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * EnemyBT.sightReach));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.transform.position);

        Vector3 forwardVector = transform.forward;
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(forwardVector, vectorToPlayer);

        if (angle < _fovRange / 2 && Vector3.Distance(transform.position, player.transform.position) < EnemyBT.sightReach)
        {
            bool isHitPlayer = Physics.Linecast(transform.position, player.transform.position, _playerMask);
            bool isHitWall = Physics.Linecast(transform.position, player.transform.position, _wallMask);

            if (isHitPlayer && !isHitWall)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, player.transform.position);
            }
        }
        Gizmos.color = Color.red;
        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(new Vector3(0f, 0f, 1f), new Vector3(0.8f, 0.8f, 0.8f));
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 400, 20), "Number of possible waypoints: " + EnemyBT.visibleWaypoints.Count);
        GUI.Label(new Rect(10, 20, 400, 20), "Last waypoint visited: " + EnemyBT.lastWaypointVisited);
        GUI.Label(new Rect(10, 30, 400, 20), "Current waypoint: " + EnemyBT.currentWaypoint);
    }
}
