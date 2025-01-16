using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the player's shooting mechanic, including projectile instantiation,
/// cooldowns, and mana management.
/// </summary>
public class Shooting : MonoBehaviour
{
    public GameObject projetilePrefabs; // Prefab for the projectile
    public int damageToGive; // Damage dealt by each projectile

    private float nextFire; // Time for the next shot
    private float fireRate = 1f; // Rate of fire

    /// <summary>
    /// Checks for input and triggers shooting if conditions are met.
    /// </summary>
    private void Update()
    {
        Shoot();
    }

    /// <summary>
    /// Spawns projectiles in multiple directions when the player presses the shoot button.
    /// </summary>
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire &&
            PlayerHealthManager.instance.GetCurrentMP() != 0)
        {
            nextFire = Time.time + fireRate;

            for (int i = 0; i < 8; i++)
            {
                Instantiate(projetilePrefabs, transform.position +
                    new Vector3(0, 0, 1), transform.rotation *
                    Quaternion.Euler(new Vector3(0, 0, 45 * i)));
            }

            PlayerHealthManager.instance.HurtPlayerMana(10); // Deduct mana
            AudioManager.instance.Play("ShootFromPlayer"); // Play shooting sound
        }
    }
}
