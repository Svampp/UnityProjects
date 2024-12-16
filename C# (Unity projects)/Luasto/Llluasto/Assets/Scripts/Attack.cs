using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damageToGive;  // The amount of damage the projectile will deal to enemies

    [SerializeField]
    private int moveSpeed = 2;  // Speed at which the projectile moves

    private int timeToWait = 5;  // Time in seconds before the projectile is destroyed

    private void Start()
    {
        StartCoroutine(DestroyProjectile());  // Start the coroutine to destroy the projectile after timeToWait seconds
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * moveSpeed;  // Move the projectile in the direction it is facing
    }

    // Coroutine to destroy the projectile after 'timeToWait' seconds
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);  // Destroy the projectile after the specified wait time
    }

    // This method is triggered when the projectile collides with another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // Check if the collision is with an enemy
        {
            EnemyHealthManager enemyHealthManager = collision.GetComponent<EnemyHealthManager>();  // Get the enemy's health manager
            enemyHealthManager.HurtEnemy(damageToGive);  // Deal damage to the enemy
        }
    }
}
