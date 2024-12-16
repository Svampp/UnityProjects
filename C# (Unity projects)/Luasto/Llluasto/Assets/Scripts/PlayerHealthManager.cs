using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    // Singleton instance for easy access to the PlayerHealthManager
    public static PlayerHealthManager instance;

    // Player's character name (for debugging or UI display purposes)
    public string charName;

    // Current health of the player
    public float currentHP = 0;

    // Maximum health the player can have (set in the inspector)
    [SerializeField]
    private float maxHP = 100;

    // Represents the player's current cake (may relate to a specific mechanic or item)
    public float currentCake;

    // Reference to the UI elements for displaying the health bar and text
    public Image playerHealthbar;
    public TextMeshProUGUI HPText;

    // Speed at which the health bar lerps (smoothly transitions) from one value to another
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Set the singleton instance to this object for easy global access
        instance = this;

        // Set the player's current health to the maximum at the start
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // Continuously check the player's status (health bar update)
        CheckPlayerStatus();
    }

    // Checks and updates the player's health status on the UI
    private void CheckPlayerStatus()
    {
        // If the current health is not equal to the health bar fill amount, update the health bar
        if (currentHP != playerHealthbar.fillAmount)
        {
            // Smoothly transition the health bar's fill amount over time
            playerHealthbar.fillAmount = Mathf.Lerp(playerHealthbar.fillAmount,
                currentHP / maxHP, Time.deltaTime * lerpSpeed);

            // Update the health text UI to display the current health in relation to max health
            HPText.text = "HP: " + Mathf.Round(playerHealthbar.fillAmount * 100) + " / "
                + maxHP;
        }
    }

    // Adds health to the player (used when picking up health items, etc.)
    public void AddPlayerHealth(int healthAmount)
    {
        // Increase current health by the given amount
        currentHP += healthAmount;

        // Ensure current health doesn't exceed max health
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // Reduces the player's health by the given damage amount (used when taking damage)
    public void HurtPlayer(int damageToGive)
    {
        // Decrease current health by the damage amount
        currentHP -= damageToGive;

        // If health drops to zero or below, the player dies
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // Handles the player's death
    private void Die()
    {
        // Output death message to console (can be expanded for more effects, like animations)
        print(charName + " die");

        // Destroy the player's game object (could be replaced with death animation or respawn logic)
        Destroy(gameObject);
    }

    // Resets the player's health to the maximum
    public void SetMaxHP()
    {
        currentHP = maxHP;
    }
}
