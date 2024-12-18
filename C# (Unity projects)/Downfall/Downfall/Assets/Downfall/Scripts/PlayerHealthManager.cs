using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    // Singleton instance for global access
    public static PlayerHealthManager instance;

    // Player's name
    public string charName;

    // Player's current level
    public int playerLevel = 1;

    // Current and maximum health points
    public float currentHP = 0;
    [SerializeField]
    private float maxHP = 100;

    // Current and maximum experience points
    [SerializeField]
    public float currentEXP;
    public float maxEXP = 0;

    // An additional resource (e.g., cake points, optional)
    public float currentCake;

    // UI elements for health and experience bars
    public Image playerHealthbar;
    public TextMeshProUGUI HPText;
    public float lerpSpeed;

    public Image EXPbar;
    public TextMeshProUGUI EXPText;
    public TextMeshProUGUI playerLevelText;

    void Start()
    {
        // Initialize the singleton instance
        instance = this;

        // Set starting HP
        currentHP = 50;

        // Calculate the initial max EXP needed for leveling up
        maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));

        // Update the UI for the player level
        playerLevelText.text = playerLevel.ToString();
    }

    void Update()
    {
        // Continuously check and update the player's status
        CheckPlayerStatus();
    }

    private void CheckPlayerStatus()
    {
        // Smoothly update the health bar to match current HP
        if (currentHP != playerHealthbar.fillAmount)
        {
            playerHealthbar.fillAmount = Mathf.Lerp(playerHealthbar.fillAmount,
                currentHP / maxHP, Time.deltaTime * lerpSpeed);

            // Update the health text
            HPText.text = "HP: " + Mathf.Round(playerHealthbar.fillAmount * 100) + " / " + maxHP;
        }

        // Smoothly update the experience bar to match current EXP
        if (currentEXP != EXPbar.fillAmount)
        {
            EXPbar.fillAmount = Mathf.Lerp(EXPbar.fillAmount,
                currentEXP / maxEXP, Time.deltaTime * lerpSpeed);

            // Update the experience text
            EXPText.text = "EXP: " + currentEXP + " / " + maxEXP;
        }

        // Level up the player if their current EXP exceeds the max EXP threshold
        if (currentEXP >= maxEXP)
        {
            playerLevel += 1;

            // Update the player level UI
            playerLevelText.text = playerLevel.ToString();

            // Recalculate the max EXP for the next level
            maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));

            // Reset the current EXP
            currentEXP = 0;

            // Notify the level-up event
            print("YEPEE LEVELUP!");
        }
    }

    // Adds health to the player, capping it at the maximum HP
    public void AddPlayerHealth(int healthAmount)
    {
        currentHP += healthAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // Damages the player and checks if their HP drops to zero
    public void HurtPlayer(int damageToGive)
    {
        currentHP -= damageToGive;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // Handles player death
    private void Die()
    {
        print(charName + " die");
        Destroy(gameObject); // Destroys the player object upon death
    }

    // Fully restores the player's health to the maximum HP
    public void SetMaxHP()
    {
        currentHP = maxHP;
    }

    // Adds experience points to the player
    public void AddPlayerEXP(int EXPammount)
    {
        currentEXP += EXPammount;
    }
}
