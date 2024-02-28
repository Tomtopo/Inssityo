using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class HitTarget : Node
{
    private Transform _transform;
    private GameObject _player;

    private PlayerHealth _playerHealth;

    private LayerMask _playerMask;

    private float _counter = 0f;

    private bool lookAtTarget = false;
    private bool isHit = false;

    public HitTarget(Transform transform, GameObject player, PlayerHealth playerHealth, LayerMask playerMask)
    {
        _transform = transform;
        _player = player;
        _playerHealth = playerHealth;
        _playerMask = playerMask;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if(target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        //Turn towards target before charching an attack
        if(!lookAtTarget)
        {
            _transform.LookAt(new Vector3(target.position.x, _transform.position.y, target.position.z));
            lookAtTarget = true;
        }

        _counter += Time.deltaTime;
        //Charge an attack
        if (_counter >= 1f && !isHit)
        {

            // Chck whether the target was hit
            Quaternion q = Quaternion.identity;
            Collider[] hit = Physics.OverlapBox(_transform.position + _transform.forward * 1f, new Vector3(0.8f, 0.8f, 0.8f), q, _playerMask);
            if (hit.Length == 1)
            {
                //Debug.Log(hit[0].transform);
                Debug.Log("ouch");
                _playerHealth.TakeDamage(1);
                _player.GetComponent<Rigidbody>().AddForce((target.transform.position - _transform.position).normalized * 10f, ForceMode.Impulse);
            }
            isHit = true;
        }
        //Cooldown after an attack
        else if (_counter >= 2f)
        {
            EnemyBT.attackRange = 2f;
            isHit = false;
            lookAtTarget = false;
            _counter = 0f;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }

}
