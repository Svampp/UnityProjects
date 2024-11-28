using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Speed at which the enemy moves toward the player
    public float speed = 3.0f;

    // Reference to the enemy's Rigidbody for physics-based movement
    private Rigidbody enemyRb;

    // Reference to the player GameObject to target
    private GameObject player;

    void Start()
    {
        // Get the Rigidbody component attached to the enemy
        enemyRb = GetComponent<Rigidbody>();

        // Find the player GameObject in the scene by its name
        player = GameObject.Find("Player");
    }

    void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Apply a force to the enemy Rigidbody to move it toward the player
        enemyRb.AddForce(lookDirection * speed);

        // Check if the enemy has fallen below a certain threshold
        if (transform.position.y < -10)
        {
            // Destroy the enemy GameObject to clean up
            Destroy(gameObject);
        }
    }
}
