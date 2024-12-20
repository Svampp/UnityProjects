using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Reference to the ScoreManager to update the score
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
            // Increase the score by 1
            scoreManager.Score += 1;

            // Destroy the asteroid object after collision
            Destroy(collision.gameObject);
        }
    }
}
