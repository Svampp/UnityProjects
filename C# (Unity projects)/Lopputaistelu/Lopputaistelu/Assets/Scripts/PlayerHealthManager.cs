using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health, experience, and level progression.
/// Handles health regeneration, damage, and experience gain.
/// </summary>
public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance; // Singleton instance for global access.

    public string charName; // The name of the character.
    public int playerLevel = 1; // Current player level.

    public float currentHP = 0; // Current health points.
    [SerializeField]
    private float maxHP = 100; // Maximum health points.

    public float currentEXP; // Current experience points.
    public float maxEXP = 0; // Maximum experience points required for level up.

    public float currentCake; // An additional player attribute (e.g., for special items).

    public Image playerHealthbar; // UI element for health bar.
    public TextMeshProUGUI HPText; // UI text for displaying health points.
    public Image playerManabar; // UI element for mana bar (not yet implemented).
    public TextMeshProUGUI MPText; // UI text for mana points (not yet implemented).
    public float lerpSpeed; // Speed of bar value interpolation.

    public Image EXPbar; // UI element for experience bar.
    public TextMeshProUGUI EXPText; // UI text for displaying experience points.
    public TextMeshProUGUI playerLevelText; // UI text for displaying player level.

    /// <summary>
    /// Initializes the player's health and experience values at the start of the game.
    /// </summary>
    void Start()
    {
        instance = this;

        currentHP = maxHP;
        maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f)); // Calculate max EXP for level.
    }

    /// <summary>
    /// Updates the player's status, including health and experience display.
    /// </summary>
    void Update()
    {
        CheckPlayerStatus();
    }

    /// <summary>
    /// Checks and updates the player's health and experience bar UI.
    /// Handles level up when max EXP is reached.
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (currentHP != playerHealthbar.fillAmount)
        {
            playerHealthbar.fillAmount = Mathf.Lerp(playerHealthbar.fillAmount, currentHP / maxHP, Time.deltaTime * lerpSpeed);
            HPText.text = "HP: " + Mathf.Round(playerHealthbar.fillAmount * 100) + " / " + maxHP;
        }

        if (currentEXP != EXPbar.fillAmount)
        {
            EXPbar.fillAmount = Mathf.Lerp(EXPbar.fillAmount, currentEXP / maxEXP, Time.deltaTime * lerpSpeed);
            EXPText.text = "EXP: " + currentEXP + " / " + maxEXP;
        }

        if (currentEXP >= maxEXP)
        {
            playerLevel++; // Increase player level.
            playerLevelText.text = "Level: " + playerLevel.ToString();
            maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f)); // Recalculate max EXP.
            currentEXP = 0; // Reset current EXP.
        }
    }

    /// <summary>
    /// Adds health to the player up to the maximum value.
    /// </summary>
    /// <param name="healthAmount">Amount of health to add.</param>
    public void AddPlayerHealth(int healthAmount)
    {
        currentHP += healthAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    /// <summary>
    /// Reduces the player's health and triggers death if health reaches zero.
    /// </summary>
    /// <param name="damageToGive">Amount of damage to deal to the player.</param>
    public void HurtPlayer(int damageToGive)
    {
        currentHP -= damageToGive;
        AudioManager.instance.Play("HurtPlayer"); // Play hurt sound effect.
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die(); // Handle player death.
        }
    }

    /// <summary>
    /// Handles player death by notifying game systems and destroying the player object.
    /// </summary>
    public void Die()
    {
        FindObjectOfType<TimerManager>().PlayerDied();
        Debug.Log($"{charName} died.");
        Destroy(gameObject); // Destroy player object.
    }

    /// <summary>
    /// Fully restores the player's health to the maximum value.
    /// </summary>
    public void SetMaxHP()
    {
        currentHP = maxHP;
    }

    /// <summary>
    /// Adds experience points to the player.
    /// </summary>
    /// <param name="EXPammount">Amount of experience to add.</param>
    public void AddPlayerEXP(int EXPammount)
    {
        currentEXP += EXPammount;
    }
}
