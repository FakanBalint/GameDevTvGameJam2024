using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour,IDamageable
{

    [SerializeField]int health ;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die(); 
        }
    }

    public void Die()
    {
        Debug.Log("Dead zombie");
        Destroy(gameObject);
    }
}
