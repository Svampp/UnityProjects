using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement settings
    [Header("Move")]
    [SerializeField] private float moveSpeed; // Speed at which the player moves
    [SerializeField] private GameObject initialMap; // Reference to the initial map (for camera bounds)

    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component for physics-based movement
    private Vector2 moveDirection; // Direction of movement based on player input

    // Property to check if the player is currently moving
    public bool IsMoving { get; private set; }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the Rigidbody2D reference
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Set the camera bounds to the initial map for restricting camera movement
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
    }

    // This method is called when the player provides input for movement (via InputSystem)
    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the movement direction from the input context (e.g., WASD keys or arrow keys)
        moveDirection = context.ReadValue<Vector2>();

        // Check if the player is moving (non-zero input)
        IsMoving = moveDirection != Vector2.zero;
    }

    // Handles the player movement by changing the Rigidbody2D's position
    private void HandlePlayerMovement()
    {
        // Move the player by modifying its position based on the movement direction and speed
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    // FixedUpdate is called every fixed frame-rate frame
    private void FixedUpdate()
    {
        // Only move the player if there is movement input
        if (IsMoving)
        {
            HandlePlayerMovement();
        }
    }
}
