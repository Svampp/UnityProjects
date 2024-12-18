using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    // Speed at which the player moves
    [SerializeField] private float moveSpeed;

    // Reference to the initial map to set camera bounds at the start
    [SerializeField] private GameObject initialMap;

    // Components
    private Rigidbody2D rb; // Player's Rigidbody2D for physics-based movement
    private Vector2 moveDirection; // Direction of player movement
    private Animator animator; // Animator for controlling player animations

    // Property to check if the player is currently moving
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Set the camera bounds to match the initial map
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
    }

    // InputSystem callback for movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // Read the movement input (Vector2)
        moveDirection = context.ReadValue<Vector2>();

        // Check if the player is moving (non-zero input vector)
        IsMoving = moveDirection != Vector2.zero;

        // Update animations based on movement input
        HandleAnimations();
    }

    // Handles animation updates based on movement state and direction
    private void HandleAnimations()
    {
        if (IsMoving)
        {
            // Update animation parameters for movement direction
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("Walking", true); // Enable walking animation
        }
        else
        {
            animator.SetBool("Walking", false); // Disable walking animation
        }
    }

    // Handles player movement using Rigidbody2D
    private void HandlePlayerMovement()
    {
        // Calculate new position based on movement direction and speed
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    // FixedUpdate is used for consistent physics updates
    private void FixedUpdate()
    {
        // Perform movement only if the player is moving
        if (IsMoving)
        {
            HandlePlayerMovement();
        }
    }
}
