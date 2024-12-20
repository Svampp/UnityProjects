using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Reference to the ScoreManager to update the player's health
    private ScoreManager scoreManager;

    private void Start()
    {
        // Find and assign the ScoreManager instance in the scene
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collided with an asteroid
        if (collision.CompareTag("Asteroid"))
        {
            // Decrease the player's health by 1
            scoreManager.HP--;

            // Destroy the asteroid object upon collision
            Destroy(collision.gameObject);
        }
    }
}
