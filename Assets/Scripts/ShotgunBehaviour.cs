using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBehaviour : PistolBehaviour
{
   [SerializeField]private int amountOfShots; 
   protected override void Shoot()
   {

        if(!MagazineSize.instance.canShoot(amountOfShots))
        {
            return;
        }

    MagazineSize.instance.RemoveBullets(amountOfShots);
       for (int i = 0; i < amountOfShots; i++)
       {
            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Apply spread to the bullet direction
            float spreadAngle = Random.Range(-spreadIntensity, spreadIntensity);
            Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * bulletSpawnPoint.right;

            // Set the bullet's velocity to move it in the direction with spread
            rb.velocity = spreadDirection * (bulletSpeed+Random.Range(-spreadIntensity, spreadIntensity));
        }
    }
   
}
