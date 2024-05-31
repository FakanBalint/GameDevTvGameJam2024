using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<IPickupable>(out IPickupable pickupable)){
            pickupable.OnPickup();
        }
    }
}
