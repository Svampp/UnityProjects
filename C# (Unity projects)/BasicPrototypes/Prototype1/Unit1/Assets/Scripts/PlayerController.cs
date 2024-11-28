using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script allows the player to control a vehicle with keyboard input.
public class PlayerController : MonoBehaviour
{
    // Movement speed of the player/vehicle.
    private float speed = 15.0f;
    // Turning speed of the player/vehicle.
    private float turnSpeed = 25.0f;
    // Variable to store horizontal input (left/right).
    private float horizontalInput;
    // Variable to store vertical input (forward/backward).
    private float forwardInput;

    void Update()
    {
        // Capture horizontal input (A/D keys).
        horizontalInput = Input.GetAxis("Horizontal");
        // Capture vertical input (W/S keys).
        forwardInput = Input.GetAxis("Vertical");

        // Move the vehicle forward or backward based on vertical input.
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Rotate the vehicle left or right based on horizontal input.
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}
