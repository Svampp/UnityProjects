using UnityEngine;

public class Warp : MonoBehaviour
{
    // Fields to hold the target map GameObject and exit point for teleportation
    [Header("TELEPORT")]
    [SerializeField] private GameObject targetMap; // The map to set the camera bounds after teleportation
    [SerializeField] GameObject exitPoint; // The exit point where the player will be teleported to

    private void Awake()
    {
        // Hide the SpriteRenderer of this GameObject and its first child to make the warp zone invisible
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Store the current position of the player
            Vector3 currentPosition = collision.transform.position;

            // Get the position of the child of the exit point (where the player should appear)
            Vector3 newPosition = exitPoint.transform.GetChild(0).transform.position;

            // Ensure the Z-coordinate remains the same to avoid changing the player's depth
            newPosition.z = currentPosition.z;

            // Teleport the player to the new position
            collision.transform.position = newPosition;

            // Update the camera bounds to match the target map
            Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
        }
    }
}
