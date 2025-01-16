/// <summary>
/// Handles shooting mechanics, including instantiating bullets and setting their velocity.
/// </summary>
public class Shoot : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField] GameObject bullet; // Prefab for the bullet.
    [SerializeField] float bulletSpeed = 1f; // Speed of the bullet.
    [SerializeField] Transform projectileSpawnPoint; // Spawn point for the bullet.

    public bool IsShooting { get; set; } // Indicates if the player is currently shooting.
    Mouse mouse;

    void Start()
    {
        // Get the mouse input system.
        mouse = Mouse.current;
    }

    void Update()
    {
        // Check if the left mouse button is pressed this frame.
        IsShooting = mouse.leftButton.wasPressedThisFrame;
    }

    void FixedUpdate()
    {
        // If the player is shooting, instantiate and launch a bullet.
        if (IsShooting)
        {
            GameObject newBullet = Instantiate(bullet, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            Rigidbody rb = newBullet.GetComponent<Rigidbody>();

            // Set the bullet's velocity.
            rb.velocity = projectileSpawnPoint.forward * bulletSpeed;
        }

        // Reset the shooting state.
        IsShooting = false;
    }
}
