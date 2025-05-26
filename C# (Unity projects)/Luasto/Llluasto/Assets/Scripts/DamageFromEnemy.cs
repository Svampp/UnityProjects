using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFromEnemy : MonoBehaviour
{
    public float speed;  // Speed at which the projectile moves

    GameObject player;  // Reference to the player
    Rigidbody2D rb2d;   // Rigidbody2D component for movement
    Vector3 target, dir;  // Target position (player) and direction

    [SerializeField]
    private int damageToGive;  // Amount of damage to deal to the player

    private void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();  // Get Rigidbody2D component

        // Calculate direction towards the player
        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;  // Normalize the direction vector
        }
    }

    private void FixedUpdate()
    {
        // If the target (player) is set, move the projectile towards the player
        if (target != Vector3.zero)
        {
            // Move the projectile towards the target (player)
            rb2d.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile collides with the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerHealthManager and apply damage
            PlayerHealthManager playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                playerHealthManager.HurtPlayer(damageToGive);  // Apply damage to the player
            }

            Destroy(gameObject);  // Destroy the projectile
        }
    }

    private void OnBecameInvisible()
    {
        // Destroy the projectile if it goes off-screen
        Destroy(gameObject);
    }
}
