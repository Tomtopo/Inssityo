using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class Patrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int _currentIndex = 0;

    public Patrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_transform.position, _waypoints[_currentIndex].position) > 0.1f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_currentIndex].position, EnemyBT.speed * Time.deltaTime);
            Vector3 targetDirection = _waypoints[_currentIndex].position - _transform.position;
            float singleStep = EnemyBT.speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(_transform.forward, targetDirection, singleStep, 0.0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
           
        else
        {
            if (_currentIndex == _waypoints.Length - 1)
                _currentIndex = 0;
            else
                _currentIndex++;
        }


        return state;
    }

}
