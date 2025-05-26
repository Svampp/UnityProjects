using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of a projectile, including movement,
/// damage application, and automatic destruction after a set time.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damageToGive; // Damage dealt by the projectile
    [SerializeField]
    private int moveSpeed = 5; // Speed of the projectile
    private int timeToWait = 5; // Time before the projectile is destroyed

    [SerializeField]
    private GameObject bloodSplash; // Visual effect for hitting an enemy

    /// <summary>
    /// Starts the destruction timer for the projectile.
    /// </summary>
    private void Start()
    {
        StartCoroutine(DestroyProjectile());
    }

    /// <summary>
    /// Moves the projectile forward every frame.
    /// </summary>
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    /// <summary>
    /// Automatically destroys the projectile after a delay.
    /// </summary>
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles collisions with other objects, applying damage to enemies.
    /// </summary>
    /// <param name="collision">The collider of the object hit.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(bloodSplash, transform.position, Quaternion.identity); // Spawn blood effect
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            if (enemyHealthManager != null)
            {
                enemyHealthManager.HurtEnemy(damageToGive); // Apply damage to the enemy
            }
            Destroy(gameObject); // Destroy the projectile after impact
        }
    }
}
