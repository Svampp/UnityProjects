using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    // The amount of coins this item will add to the player's total when collected
    public int coinAmount = 1;

    // This method is called when the player collides with the item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that collided with this item has the tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Add coins to the player's total using CoinManager
            CoinManager.instance.AddCoins(coinAmount);

            // Play the "Coin" sound effect when a coin is collected
            AudioManager.instance.Play("Coin");

            // Destroy the item after it has been collected
            Destroy(gameObject);
        }
    }
}
