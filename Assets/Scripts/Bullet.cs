using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public bool shoulPierce = false;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
            damageable.TakeDamage(damage);

            if(!shoulPierce){
                Destroy(gameObject);
            }
        }
    }
}

