using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using Unity.VisualScripting;
using UnityEngine;

public class CheckTargetInFOVRange : Node
{
    private Transform _transform;

    private GameObject _player;

    private LayerMask _playerMask;
    private LayerMask _wallMask;

    //bool _isHitPlayer;
    //bool _isHitWall;

    private float _fovRange = 90f;
    public CheckTargetInFOVRange(Transform transform, GameObject player, LayerMask playerMask, LayerMask wallMask) 
    { 
        _transform = transform;
        _player = player;
        _playerMask = playerMask;
        _wallMask = wallMask;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            Vector3 forwardVector = _transform.forward;
            Vector3 vectorToPlayer = _player.transform.position - _transform.position;
            float angle = Vector3.Angle(forwardVector, vectorToPlayer);
            float distanceFromCenter = Vector3.Distance(forwardVector, vectorToPlayer);
            //Debug.Log(angle);
            if (angle < _fovRange / 2 && Vector3.Distance(_transform.position, _player.transform.position) < EnemyBT.sightReach)
            {
                bool isHitPlayer = Physics.Linecast(_transform.position, _player.transform.position, _playerMask);
                bool isHitWall = Physics.Linecast(_transform.position, _player.transform.position, _wallMask);

                if (isHitPlayer && !isHitWall)
                {
                    parent.parent.parent.SetData("target", _player.transform);
                    state = NodeState.SUCCESS;
                    return state;
                }


            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;

    }
}
