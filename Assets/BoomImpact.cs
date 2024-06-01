using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomImpact : MonoBehaviour
{
    int damage = 15;
    [SerializeField]AudioClip boomSound;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
            damageable.TakeDamage(damage);
            Explode();
        }
    }

    public void Explode()
    {
        AudioManager.instance.PlaySound(boomSound);
        Destroy(gameObject, 0.25f);
    }
}
