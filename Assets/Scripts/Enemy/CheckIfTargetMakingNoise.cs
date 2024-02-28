using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Net;
using UnityEngine.AI;

public class CheckIfTargetMakingNoise : Node
{
    Transform _transform;
    GameObject _player;

    private NavMeshAgent _agent;

    private LayerMask _playerMask;
    private LayerMask _wallMask;

    public CheckIfTargetMakingNoise(Transform transform, GameObject player, NavMeshAgent agent, LayerMask playerMask, LayerMask wallMask)
    {
        _transform = transform;
        _player = player;
        _agent = agent;
        _playerMask = playerMask;
        _wallMask = wallMask;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            if (!_player.GetComponent<PlayerMovement>().isCrouching && Vector3.Distance(_transform.position, _player.transform.position) < 5f)
            {
                bool isHitPlayer = Physics.Linecast(_transform.position, _player.transform.position, _playerMask);
                bool isHitWall = Physics.Linecast(_transform.position, _player.transform.position, _wallMask);

                if(isHitPlayer && !isHitWall)
                {
                    Debug.Log("I hear you...");
                    parent.parent.parent.SetData("target", _player.transform);
                    _agent.SetDestination(_player.transform.position);
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }

        state = NodeState.FAILURE;
        return state;





    }
}
