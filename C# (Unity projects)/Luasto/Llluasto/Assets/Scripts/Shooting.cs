using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Reference to the projectile prefab that will be instantiated
    public GameObject projetilePrefabs;

    // The amount of damage each projectile will cause
    public int damageToGive;

    // Variable to control when the next shot can be fired
    private float nextFire;

    // Rate of fire (time between shots)
    private float fireRate = 1f;

    // Update is called once per frame
    private void Update()
    {
        // Check if the player is pressing the shoot button (space bar)
        Shoot();
    }

    // Method to handle shooting projectiles
    void Shoot()
    {
        // Check if the space key is pressed and enough time has passed since the last shot
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            // Set the next time the player can shoot (current time + fire rate)
            nextFire = Time.time + fireRate;

            // Create 3 projectiles, with slightly different rotations for each one
            for (int i = 0; i < 3; i++)
            {
                // Instantiate a new projectile at the current position with a slight offset and rotation
                Instantiate(projetilePrefabs,
                            transform.position + new Vector3(0, 0, 1), // Offset the position along the Z-axis
                            transform.rotation * Quaternion.Euler(new Vector3(0, 0, 45 * i))); // Rotate each projectile slightly
            }

            // Play a shooting sound using the AudioManager instance
            AudioManager.instance.Play("Shooting");
        }
    }
}
