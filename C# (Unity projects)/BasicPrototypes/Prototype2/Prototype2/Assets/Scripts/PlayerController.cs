using UnityEngine;

// This script handles player movement within a horizontal range and shooting projectiles.
public class PlayerController : MonoBehaviour
{
    // Stores the player's horizontal input from the keyboard
    public float horizontalInput;
    // Speed at which the player moves left or right.
    public float speed = 10.0f;
    // The maximum range the player can move along the x-axis.
    public float xRange = 10.0f;

    // Reference to the projectile prefab that will be instantiated when the player shoots.
    public GameObject projectilePrefab;

    void Update()
    {
        // Restrict the player's movement to the left boundary of the xRange.
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        // Restrict the player's movement to the right boundary of the xRange.
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Get horizontal input from the player.
        horizontalInput = Input.GetAxis("Horizontal");
        // Move the player horizontally.
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        // Check if the player presses the Space key to shoot a projectile.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate a projectile at the player's current position with the prefab's rotation.
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
