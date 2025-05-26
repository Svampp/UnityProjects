using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Speed at which the camera rotates
    public float rotationSpeed;

    void Update()
    {
        // Get horizontal input from the player.
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the camera around the up (Y-axis) based on player input
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
