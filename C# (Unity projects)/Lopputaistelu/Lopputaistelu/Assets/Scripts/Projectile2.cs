using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the behavior of a secondary projectile, including damage to the player
/// and self-destruction after a delay or upon collision.
/// </summary>
public class Projectile2 : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the player upon collision.

    /// <summary>
    /// Destroys the projectile after 5 seconds if it doesn't collide with anything.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    /// <summary>
    /// Handles collision with the player, applying damage and destroying the projectile.
    /// </summary>
    /// <param name="other">The collider that the projectile hit.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthManager.instance.HurtPlayer(damage); // Apply damage to the player.
            Destroy(gameObject); // Destroy the projectile upon collision.
        }
    }
}
