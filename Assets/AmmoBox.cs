using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, IPickupable
{


    public void OnPickup()
    {
        MagazineSize.instance.AddBullets(60);
        Destroy(gameObject);
    }

}
