using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public bool shoulPierce = false;
    public GameObject impactEffect;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        

       if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
           damageable.TakeDamage(damage);
                if(impactEffect != null){
                GameObject o =  Instantiate(impactEffect, transform.position, Quaternion.identity);
                o.transform.localScale *= Mathf.Clamp(damage,1,5);
                Destroy(o, 0.25f);
            }
           
           if(!shoulPierce){
               Destroy(gameObject);
           }
       }
    }
}

