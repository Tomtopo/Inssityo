using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class LookLeftAndRight : Node
{
    private Transform _transform;

    private Vector3 _startingDirection;
    private Vector3 _leftDirection;
    private Vector3 _rightDirection;

    private bool _startingValues = false;
    private bool _lookingLeft = false;
    private bool _lookingRight = false;

    public LookLeftAndRight(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        if(!_startingValues)
        {
            _startingDirection = _transform.forward;
            _leftDirection = -_transform.right;
            _rightDirection = _transform.right;
            _startingValues = true;
        }
        float singleStep = EnemyBT.speed / 3f * Time.deltaTime;
        if(!_lookingLeft)
        {

            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, _leftDirection, singleStep, 0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if(_lookingLeft && !_lookingRight)
        {
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, _rightDirection, singleStep, 0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if(_lookingLeft && _lookingRight)
        {
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, _startingDirection, singleStep, 0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
        
        if(Vector3.Angle(_leftDirection, _transform.forward) <= 2f && !_lookingLeft)
        {
            _lookingLeft = true;
        }
        else if(Vector3.Angle(_rightDirection, _transform.forward) <= 2f && !_lookingRight)
        {
            _lookingRight = true;           
        }
        if(_lookingLeft && _lookingRight && Vector3.Angle(_startingDirection, _transform.forward) <= 2f)
        {
            _lookingLeft = false;
            _lookingRight = false;
            _startingValues = false;
            EnemyBT.isLookingLeftAndRight = false;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }

}
