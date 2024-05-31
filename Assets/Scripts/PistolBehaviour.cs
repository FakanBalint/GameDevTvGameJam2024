using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;  // Assign your bullet prefab here
    public Transform bulletSpawnPoint;  // Assign the point where the bullet should spawn from
    public float bulletSpeed = 20f;  // Speed of the bullet
    [SerializeField] private float spreadIntensity = 5f; 
    void Update()
    {
        AimAtCursor();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void AimAtCursor()
    {
        // Convert mouse position to world position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;  // Keep z-coordinate zero for 2D aiming

        // Calculate the direction from the pistol to the mouse position
        Vector3 aimDirection = (worldMousePosition - transform.position).normalized;

        // Rotate the pistol to face the cursor
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Apply spread to the bullet direction
        float spreadAngle = Random.Range(-spreadIntensity, spreadIntensity);
        Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * bulletSpawnPoint.right;

        // Set the bullet's velocity to move it in the direction with spread
        rb.velocity = spreadDirection * bulletSpeed;
    }
}