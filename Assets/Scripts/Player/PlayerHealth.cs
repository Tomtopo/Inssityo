using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public bool isDead;

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;

        if(_health <= 0)
        {
            Die();
            isDead = true;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
