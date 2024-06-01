using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour,IPickupable
{
    Rigidbody2D rb;
    [SerializeField]private GameObject impactEffect;

    public void OnPickup()
    {
        rb.AddForce(transform.up * 10, ForceMode2D.Impulse);
        rb.AddTorque(10, ForceMode2D.Impulse);
        rb.gravityScale = 1;
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable)){
           GameObject o = Instantiate(impactEffect, transform.position, Quaternion.identity);
           o.transform.localScale *= 5;
           o.GetComponent<BoomImpact>().SetDamage(20);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }


}
