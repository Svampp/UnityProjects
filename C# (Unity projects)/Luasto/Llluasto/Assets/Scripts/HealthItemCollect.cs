using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemCollect : MonoBehaviour
{
    // The amount of health this item will restore when collected
    public int healthAmount = 10;

    // This method is called when the player collides with the health item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with this item has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Add health to the player's current health using PlayerHealthManager
            PlayerHealthManager.instance.AddPlayerHealth(healthAmount);

            // Play the "HP" sound effect when the health item is collected
            AudioManager.instance.Play("HP");

            // Destroy the health item after it has been collected
            Destroy(gameObject);
        }
    }
}
