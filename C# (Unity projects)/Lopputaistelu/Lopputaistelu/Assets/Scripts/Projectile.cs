using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the behavior of a projectile, including movement, collision detection,
/// and damage application to enemies.
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damageToGive; // Damage dealt by the projectile.
    [SerializeField]
    private int moveSpeed = 5; // Movement speed of the projectile.
    [SerializeField]
    private GameObject bloodSplash; // Effect displayed upon hitting an enemy.

    private float maxLifeTime = 5f; // Maximum lifetime of the projectile.

    /// <summary>
    /// Starts the destruction timer for the projectile.
    /// </summary>
    private void Start()
    {
        StartCoroutine(DestroyProjectileAfterTime());
    }

    /// <summary>
    /// Moves the projectile forward each frame.
    /// </summary>
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    /// <summary>
    /// Destroys the projectile after it has existed for its maximum lifetime.
    /// </summary>
    private IEnumerator DestroyProjectileAfterTime()
    {
        yield return new WaitForSeconds(maxLifeTime);
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles collision with enemies, applying damage and displaying effects.
    /// </summary>
    /// <param name="collision">The collider that the projectile hit.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3 enemyPosition = collision.transform.position;

            Instantiate(bloodSplash, enemyPosition, Quaternion.identity); // Display blood splash effect.

            // Apply damage to the enemy.
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();
            if (enemyHealthManager != null)
            {
                enemyHealthManager.HurtEnemy(damageToGive);
            }

            Destroy(gameObject); // Destroy the projectile after hitting the enemy.
        }
    }
}
