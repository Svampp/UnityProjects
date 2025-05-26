using UnityEngine;

// This script is used to destroy objects that move out of a specified boundary.
public class DestroyOutOfBounds : MonoBehaviour
{
    // Upper boundary of the play area along the z-axis.
    private float topBound = 30;
    // Lower boundary of the play area along the z-axis.
    private float lowerBound = -10;

    void Update()
    {
        // Check if the object has moved beyond the top boundary.
        if (transform.position.z > topBound)
        {
            // Destroy the object.
            Destroy(gameObject);
        }
        // Check if the object has moved below the lower boundary.
        else if (transform.position.z < lowerBound)
        {
            // Log a "Game Over" message to the console.
            Debug.Log("Game Over!");
            // Destroy the object.
            Destroy(gameObject);
        }
    }
}
