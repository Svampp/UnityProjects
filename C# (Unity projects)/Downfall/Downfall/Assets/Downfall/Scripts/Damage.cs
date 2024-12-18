using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Speed of the damaging object
    public float speed;

    // Reference to the player object
    GameObject player;

    // Rigidbody2D component for physics-based movement
    Rigidbody2D rb2d;

    // Target position and direction of movement
    Vector3 target, dir;

    // Damage value to apply to the player
    [SerializeField]
    private int damageToGive;

    private void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Get the Rigidbody2D component attached to this object
        rb2d = GetComponent<Rigidbody2D>();

        // Calculate the direction toward the player if the player exists
        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized; // Normalize to get the unit vector
        }
    }

    private void FixedUpdate()
    {
        // Move the object toward the target position if a target is set
        if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collided with the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerHealthManager component from the player object
            PlayerHealthManager playerHealthManager = collision.GetComponent<PlayerHealthManager>();

            // If the PlayerHealthManager exists, apply damage
            if (playerHealthManager != null)
            {
                playerHealthManager.HurtPlayer(damageToGive);
            }

            // Destroy this object after damaging the player
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Destroy the object when it goes off-screen to save resources
        Destroy(gameObject);
    }
}
