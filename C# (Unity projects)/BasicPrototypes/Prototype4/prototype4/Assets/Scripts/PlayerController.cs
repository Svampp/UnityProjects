using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Reference to the player's Rigidbody for physics-based movement
    private Rigidbody playerRb;

    // Reference to the focal point for directional movement
    private GameObject focalPoint;

    // Strength of the force applied when the player has a powerup
    private float powerUpStrength = 20.0f;

    // Player's movement speed
    public float speed = 5.0f;

    // Flag indicating if the player currently has a powerup
    public bool hasPowerup = false;

    // Reference to the powerup indicator object
    public GameObject powerupIndicator;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();

        // Find the "FocalPoint" object in the scene to determine movement direction
        focalPoint = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        // Get vertical input
        float forwardInput = Input.GetAxis("Vertical");

        // Add force to the player Rigidbody in the forward direction of the focal point
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // Update the position of the powerup indicator to follow the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    // Trigger detection for collecting powerups
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Powerup" tag
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true; // Enable powerup state
            powerupIndicator.gameObject.SetActive(true); // Show the powerup indicator
            Destroy(other.gameObject); // Remove the powerup object from the scene

            // Start the powerup countdown coroutine
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Coroutine to handle the powerup duration
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7); // Wait for 7 seconds
        hasPowerup = false; // Disable powerup state
        powerupIndicator.gameObject.SetActive(false); // Hide the powerup indicator
    }

    // Collision detection for interacting with enemies
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Enemy" and the player has a powerup
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Get the Rigidbody of the enemy object
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // Calculate the direction to push the enemy away from the player
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            // Apply an impulse force to the enemy to push it away
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);

            // Log the collision information for debugging
            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
