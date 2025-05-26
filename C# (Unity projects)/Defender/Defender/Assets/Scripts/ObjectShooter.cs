using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectShooter : MonoBehaviour
{
    [Header("AMMUS")]
    // The bullet prefab to be instantiated when shooting
    [SerializeField] private GameObject bullletPrefab;

    // The speed at which the bullet will move
    [SerializeField] private float bulletSpeed;

    // The point from which the bullet will be shot
    [SerializeField] private Transform shootPoint;

    // The rate at which bullets can be fired (fire rate)
    [SerializeField] private float fireRate = 0.5f;

    // Time tracking for when the next shot can be fired
    private float nextFire = 0f;

    // Property to check if the player is currently shooting
    public bool IsShooting { get; set; }

    // Keyboard input reference
    private Keyboard keyboard;

    private void Start()
    {
        // Initialize the keyboard input system to track key states
        keyboard = Keyboard.current;
    }

    private void Update()
    {
        // Check if the space bar is pressed to start shooting
        IsShooting = keyboard.spaceKey.IsPressed();
    }

    private void FixedUpdate()
    {
        // If shooting is active and enough time has passed (based on fire rate), shoot a bullet
        if (IsShooting && Time.time > nextFire)
        {
            // Set the next available fire time based on fire rate
            nextFire = Time.time + fireRate;

            // Instantiate the bullet at the shoot point with no rotation
            GameObject newBullet = Instantiate(bullletPrefab, shootPoint.position, Quaternion.identity);

            // Get the Rigidbody2D component of the bullet to apply force for movement
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

            // Apply a force to the bullet in the direction of the object’s right side (transform.right)
            rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }

        // Reset the shooting state to false to prevent continuous shooting in one frame
        IsShooting = false;
    }
}
