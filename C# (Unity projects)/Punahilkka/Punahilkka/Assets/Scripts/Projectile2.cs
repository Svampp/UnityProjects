using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles homing projectile behavior, targeting the player and applying damage on impact.
/// </summary>
public class Projectile2 : MonoBehaviour
{
    public float speed; // Speed of the projectile

    private GameObject player; // Reference to the player object
    private Rigidbody2D rb2d; // Rigidbody for movement
    private Vector3 target, dir; // Target position and direction to move

    [SerializeField]
    private int damageToGive; // Damage dealt by the projectile

    /// <summary>
    /// Initializes the projectile's direction toward the player.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();

        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
    }

    /// <summary>
    /// Moves the projectile toward the player every frame.
    /// </summary>
    private void FixedUpdate()
    {
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Applies damage to the player upon collision and destroys the projectile.
    /// </summary>
    /// <param name="collision">The collider of the object hit.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthManager playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                playerHealthManager.HurtPlayer(damageToGive);
            }
            Destroy(gameObject); // Destroy the projectile
        }
    }

    /// <summary>
    /// Destroys the projectile when it goes off-screen.
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
