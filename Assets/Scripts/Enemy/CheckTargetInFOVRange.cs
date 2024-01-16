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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(_transform.position, _transform.forward * EnemyBT.reach);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(_transform.position, (_transform.position + _player.transform.position).normalized * EnemyBT.reach);
    //    if(_isHitPlayer && !_isHitWall)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(_transform.position, (_transform.position + _player.transform.position).normalized * EnemyBT.reach);
    //    }
    //}

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            Vector3 forwardVector = _transform.forward;
            Vector3 vectorToPlayer = _player.transform.position - _transform.position;
            float angle = Vector3.Angle(forwardVector, vectorToPlayer);
            float distanceFromCenter = Vector3.Distance(forwardVector, vectorToPlayer);
            //Debug.Log(angle);
            if (angle < _fovRange / 2 && Vector3.Distance(_transform.position, _player.transform.position) < EnemyBT.reach)
            {
                bool isHitPlayer = Physics.Linecast(_transform.position, _player.transform.position, _playerMask);
                bool isHitWall = Physics.Linecast(_transform.position, _player.transform.position, _wallMask);

                if (isHitPlayer && !isHitWall)
                {
                    Debug.Log("näkee");
                    parent.parent.SetData("target", _player.transform);
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
