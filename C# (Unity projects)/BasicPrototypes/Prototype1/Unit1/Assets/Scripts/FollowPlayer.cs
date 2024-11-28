using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script makes the camera follow a target player with an offset.
public class FollowPlayer : MonoBehaviour
{
    //The player GameObject to follow.
    public GameObject player;
    // Offset position of the camera relative to the player.
    private Vector3 offset = new Vector3(0, 5, -7);

    // LateUpdate is called once per frame, after all Update methods have been processed.
    // This ensures the camera updates its position after the player has moved.
    void LateUpdate()
    {
        // Set the position of the camera to the player's position plus the offset.
        // This creates the "following" effect.
        transform.position=player.transform.position + offset;
    }
}
