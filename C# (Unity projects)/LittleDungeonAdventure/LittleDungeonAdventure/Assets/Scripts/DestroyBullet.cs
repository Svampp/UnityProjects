/// <summary>
/// Destroys the bullet after a delay.
/// </summary>
public class DestroyBullet : MonoBehaviour
{
    // Time in seconds before the bullet is destroyed.
    [SerializeField] float onScreenDelay = 3f;

    void Start()
    {
        // Automatically destroy the bullet after the delay.
        Destroy(gameObject, onScreenDelay);
    }
}
