using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class HitTarget : Node
{
    private Transform _transform;
    private GameObject _player;

    private PlayerHealth _playerHealth;

    private float _counter = 0f;

    public HitTarget(Transform transform, GameObject player, PlayerHealth playerHealth)
    {
        _transform = transform;
        _player = player;
        _playerHealth = playerHealth;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if(target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _transform.LookAt(target);
        _counter += Time.deltaTime;
        if(_counter >= 1f)
        {
            _counter = 0f;
            _playerHealth.TakeDamage(1);
            _player.GetComponent<Rigidbody>().AddForce(_transform.forward * 10f, ForceMode.Impulse);
            if(_playerHealth.isDead)
            {
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.RUNNING; 
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
