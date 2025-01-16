using System.Collections;
using UnityEngine;

/// <summary>
/// Handles teleportation between maps, moving the player to a new location
/// and adjusting the camera bounds accordingly.
/// </summary>
public class Warp : MonoBehaviour
{
    [Header("TELEPORT")]
    [SerializeField] private GameObject targetMap; // The map to set the camera bounds to after teleportation.
    [SerializeField] private GameObject exitPoint; // The exit point where the player will be teleported.

    /// <summary>
    /// Hides the warp object's visual indicators during gameplay.
    /// </summary>
    private void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false; // Hide the main sprite.
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; // Hide the child sprite.
    }

    /// <summary>
    /// Teleports the player to the exit point when they enter the warp trigger.
    /// </summary>
    /// <param name="collision">The collider that entered the warp trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get the current position of the player and adjust the Z coordinate.
            Vector3 currentPosition = collision.transform.position;
            Vector3 newPosition = exitPoint.transform.GetChild(0).transform.position;
            newPosition.z = currentPosition.z;

            collision.transform.position = newPosition; // Teleport the player.

            // Update the camera bounds to match the new map.
            Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
        }
    }
}
