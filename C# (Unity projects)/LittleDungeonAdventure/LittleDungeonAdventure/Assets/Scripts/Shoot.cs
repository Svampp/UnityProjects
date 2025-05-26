/// <summary>
/// Handles shooting mechanics, including bullet instantiation and movement.
/// </summary>
public class Shoot : MonoBehaviour
{
    [Header("Shoot Settings")]
    [SerializeField] GameObject bullet; // Prefab for the bullet to be instantiated.
    [SerializeField] float bulletSpeed = 1f; // Speed of the bullet after being fired.
    [SerializeField] Transform projectileSpawnPoint; // The point from which the bullet will spawn.

    public bool IsShooting { get; set; } // Indicates whether the player is shooting.
    Mouse mouse; // Reference to the mouse input system.

    void Start()
    {
        // Initialize the mouse input system.
        mouse = Mouse.current;
    }

    void Update()
    {
        // Check if the left mouse button was pressed this frame.
        IsShooting = mouse.leftButton.wasPressedThisFrame;
    }

    void FixedUpdate()
    {
        // If the player is shooting, instantiate and launch a bullet.
        if (IsShooting)
        {
            // Create a new bullet at the spawn point with the same rotation.
            GameObject newBullet = Instantiate(bullet, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Get the Rigidbody component of the bullet and set its velocity.
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.velocity = projectileSpawnPoint.forward * bulletSpeed;
        }

        // Reset the shooting state.
        IsShooting = false;
    }
}
