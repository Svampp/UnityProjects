using UnityEngine;

/// <summary>
/// Handles the destruction of bullets after a delay or upon collision with an interactable object.
/// </summary>
public class DestroyBullet : MonoBehaviour
{
    // Time in seconds before the bullet is destroyed after appearing on screen.
    [SerializeField] private float onScreenDelay = 3f;

    void Start()
    {
        // Destroy the bullet after the specified delay.
        Destroy(gameObject, onScreenDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the bullet collides with implements the IInteractable interface.
        if (other.TryGetComponent(out IInteractable interactable))
        {
            // Trigger the interaction behavior.
            interactable.Interact();

            // Destroy the bullet after interacting.
            Destroy(gameObject);
        }
    }
}
