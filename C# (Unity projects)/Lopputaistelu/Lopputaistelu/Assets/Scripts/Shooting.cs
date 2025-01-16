using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the player's shooting mechanic, including projectile spawning and firing rate.
/// </summary>
public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefabs; // Prefab for the projectiles fired by the player.

    public int damageToGive; // Damage dealt by each projectile.

    private float nextFire; // Time at which the player can fire the next shot.
    private float fireRate = 1f; // Time interval between consecutive shots.

    /// <summary>
    /// Updates the shooting mechanic every frame.
    /// </summary>
    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// Fires projectiles in multiple directions if the spacebar is pressed and the fire rate allows it.
    /// </summary>
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate; // Update the next fire time.

            // Spawn four projectiles in different directions (90° apart).
            for (int i = 0; i < 4; i++)
            {
                Instantiate(
                    projectilePrefabs,
                    transform.position + new Vector3(0, 0, 1), // Slightly offset from the player's position.
                    transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90 * i)) // Rotate each projectile by 90°.
                );
            }
        }
    }
}
