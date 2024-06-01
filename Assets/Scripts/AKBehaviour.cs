using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKBehaviour : PistolBehaviour
{

    [SerializeField]float fireRate;
    bool canShoot = true;
    protected override void Update()
    {
        
        AimAtCursor();

        //shoot while the left mouse button is pressed with the fire rate
        while (Input.GetMouseButton(0)&&MagazineSize.instance.canShoot(requiredBullets)&&canShoot)
        {
            Shoot();
            StartCoroutine(FireRate());
        }
    }

    IEnumerator FireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
