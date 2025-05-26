using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles player movement, animations, and interactions.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed; // Speed of the player's movement.
    [SerializeField] private GameObject initialMap; // Reference to the initial map for camera bounds.

    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component.
    private Vector2 moveDirection; // Direction of player movement.
    private Animator animator; // Reference to the Animator component.

    // Property to track whether the player is currently moving.
    public bool IsMoving { get; private set; }

    /// <summary>
    /// Initializes references to Rigidbody2D and Animator components.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets up the camera bounds and starts background audio.
    /// </summary>
    private void Start()
    {
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap); // Set camera bounds.
        AudioManager.instance.Play("Back"); // Play background audio.
    }

    /// <summary>
    /// Handles movement input from the player.
    /// </summary>
    /// <param name="context">Input context for movement.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>(); // Get movement direction.
        IsMoving = moveDirection != Vector2.zero; // Check if the player is moving.

        HandleAnimations(); // Update animations based on movement.
    }

    /// <summary>
    /// Updates animations based on the player's movement state.
    /// </summary>
    private void HandleAnimations()
    {
        if (IsMoving)
        {
            animator.SetFloat("MoveX", moveDirection.x); // Update horizontal animation.
            animator.SetFloat("MoveY", moveDirection.y); // Update vertical animation.
            animator.SetBool("Walking", true); // Set walking animation.
        }
        else
        {
            animator.SetBool("Walking", false); // Stop walking animation.
        }
    }

    /// <summary>
    /// Moves the player based on input and speed.
    /// </summary>
    private void HandlePlayerMovement()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// FixedUpdate is called on a fixed time interval for physics calculations.
    /// </summary>
    private void FixedUpdate()
    {
        if (IsMoving)
        {
            HandlePlayerMovement(); // Apply movement if the player is moving.
        }
    }
}
