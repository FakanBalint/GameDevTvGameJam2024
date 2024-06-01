using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour,IDamageable
{
    [SerializeField]AudioClip hitSound;
    [SerializeField]AudioClip dieSound;
    [SerializeField]int health ;
    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.instance.PlaySound(hitSound);
        if (health <= 0)
        {
            health = 0;
            Die(); 
        }
    }

    public void Die()
    {
        Score.instance.IncreaseScore();
        AudioManager.instance.PlaySound(dieSound);
        Destroy(gameObject);
    }
}
