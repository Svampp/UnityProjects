using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player movement and animations based on input.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed; // Movement speed
    [SerializeField] private GameObject initialMap; // Initial map for camera bounds

    private Rigidbody2D rb; // Player Rigidbody
    private Vector2 moveDirection; // Movement direction vector
    private Animator animator; // Animator for movement animations

    public bool IsMoving { get; private set; } // Indicates if the player is moving

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
    }

    /// <summary>
    /// Processes movement input from the player.
    /// </summary>
    /// <param name="context">Input context for movement.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        IsMoving = moveDirection != Vector2.zero;

        HandleAnimations();
    }

    /// <summary>
    /// Updates movement animations based on input.
    /// </summary>
    private void HandleAnimations()
    {
        if (IsMoving)
        {
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    /// <summary>
    /// Moves the player based on the current direction and speed.
    /// </summary>
    private void HandlePlayerMovement()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            HandlePlayerMovement();
        }
    }
}
