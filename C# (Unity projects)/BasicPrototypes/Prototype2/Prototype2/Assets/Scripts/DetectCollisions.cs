using UnityEngine;

// This script detects collisions between two GameObjects using triggers and destroys both objects upon collision.
public class DetectCollisions : MonoBehaviour
{
    // Called when another Collider enters the trigger zone of this GameObject.
    private void OnTriggerEnter(Collider other)
    {
        // Destroy this GameObject.
        Destroy(gameObject);
        // Destroy the other GameObject.
        Destroy(other.gameObject);
    }
}
