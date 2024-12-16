using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;  // Singleton instance of CoinManager

    private int totalCoins = 0;  // Total number of coins collected
    public TextMeshProUGUI coinText;  // UI Text component to display the coin count

    // Ensures that only one instance of CoinManager exists in the game
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;  // Assign this instance to the static instance
        }
        else
        {
            Destroy(gameObject);  // Destroy this object if an instance already exists
        }
    }

    private void Start()
    {
        UpdateCoinUI();  // Initial UI update with the starting coin count
    }

    // Method to add coins to the total and update the UI
    public void AddCoins(int amount)
    {
        totalCoins += amount;  // Add the specified amount of coins
        UpdateCoinUI();  // Update the UI to reflect the new coin count
    }

    // Method to update the coin UI text
    private void UpdateCoinUI()
    {
        coinText.text = totalCoins.ToString();  // Update the coin text in the UI
    }
}
