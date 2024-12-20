using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    [Header("Pyöritys")]
    // The speed at which the cannon will rotate
    [SerializeField] private float rotateSpeed;

    // Input value for rotating the cannon
    private float spinInput;

    // Reference to the Rigidbody2D component for adding torque
    private Rigidbody2D rb;

    private void Awake()
    {
        // Initialize the Rigidbody2D component when the script starts
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get the horizontal input axis (e.g., from the arrow keys or A/D keys) for rotation
        spinInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Apply torque to the cannon based on the input value and rotation speed
        // The negative sign is used to invert the direction of rotation
        rb.AddTorque(-spinInput * rotateSpeed);
    }
}
