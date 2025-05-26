using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    [Header("TELEPORT")]
    // Target map where the player will be teleported to
    [SerializeField] private GameObject targetMap;

    // The exit point where the player will appear after teleportation
    [SerializeField] GameObject exitPoint;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Disable the SpriteRenderer for this object and its child object to make them invisible
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }

    // Called when another collider enters this object's trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding with this trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Save the player's current position
            Vector3 currentPosition = collision.transform.position;

            // Get the position of the exit point (child of the exitPoint GameObject)
            Vector3 newPosition = exitPoint.transform.GetChild(0).transform.position;

            // Ensure the player's z position remains the same (since 2D games often ignore the z-axis)
            newPosition.z = currentPosition.z;

            // Teleport the player to the new position at the exit point
            collision.transform.position = newPosition;

            // Update the camera's bounds to match the target map (likely for camera positioning)
            Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
        }
    }
}
