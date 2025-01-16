using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health, mana, experience, and level progression.
/// Includes methods for taking damage, healing, and leveling up.
/// </summary>
public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    public string charName; // Name of the character
    public int playerLevel = 1; // Current player level

    public float currentHP = 0; // Current health
    [SerializeField] private float maxHP = 100; // Maximum health

    public float currentMP = 0; // Current mana
    [SerializeField] private float maxMP = 100; // Maximum mana

    public float currentEXP; // Current experience points
    public float maxEXP = 0; // Required experience for next level

    public float currentCake; // Placeholder for additional gameplay logic

    public Image playerHealthbar; // UI for health bar
    public TextMeshProUGUI HPText; // UI for health text
    public Image playerManabar; // UI for mana bar
    public TextMeshProUGUI MPText; // UI for mana text
    public float lerpSpeed; // Speed of bar interpolation

    public Image EXPbar; // UI for experience bar
    public TextMeshProUGUI EXPText; // UI for experience text
    public TextMeshProUGUI playerLevelText; // UI for level text

    private void Start()
    {
        instance = this;

        currentHP = maxHP;
        currentMP = maxMP;

        maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));
    }

    private void Update()
    {
        CheckPlayerStatus(); // Update UI elements and check for level up
    }

    /// <summary>
    /// Updates UI bars and text for health, mana, and experience.
    /// Handles leveling up when experience reaches the maximum.
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (currentHP != playerHealthbar.fillAmount)
        {
            playerHealthbar.fillAmount = Mathf.Lerp(playerHealthbar.fillAmount,
                currentHP / maxHP, Time.deltaTime * lerpSpeed);
            HPText.text = $"HP: {Mathf.Round(playerHealthbar.fillAmount * 100)} / {maxHP}";
        }

        if (currentMP != playerManabar.fillAmount)
        {
            playerManabar.fillAmount = Mathf.Lerp(playerManabar.fillAmount,
                currentMP / maxMP, Time.deltaTime * lerpSpeed);
            MPText.text = $"MP: {Mathf.Round(playerManabar.fillAmount * 100)} / {maxMP}";
        }

        if (currentEXP != EXPbar.fillAmount)
        {
            EXPbar.fillAmount = Mathf.Lerp(EXPbar.fillAmount,
                currentEXP / maxEXP, Time.deltaTime * lerpSpeed);
            EXPText.text = $"EXP: {currentEXP} / {maxEXP}";
        }

        if (currentEXP >= maxEXP)
        {
            LevelUp(); // Handle level up logic
        }
    }

    /// <summary>
    /// Increases the player's level and resets experience.
    /// </summary>
    private void LevelUp()
    {
        playerLevel += 1;
        playerLevelText.text = $"Level: {playerLevel}";
        maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));
        currentEXP = 0;

        Debug.Log("Level Up!");
        AudioManager.instance.Play("LevelUp");
    }

    public void AddPlayerHealth(int healthAmount)
    {
        currentHP = Mathf.Min(currentHP + healthAmount, maxHP);
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHP -= damageToGive;
        AudioManager.instance.Play("HurtPlayer");
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{charName} died.");
        AudioManager.instance.StopPlay("Background");
        AudioManager.instance.Play("PlayerDie");
        Destroy(gameObject);
    }

    public void AddPlayerMana(int manaAmount)
    {
        currentMP = Mathf.Min(currentMP + manaAmount, maxMP);
    }

    public void HurtPlayerMana(int damageToGive)
    {
        currentMP = Mathf.Max(currentMP - damageToGive, 0);
    }

    public void SetMaxHP()
    {
        currentHP = maxHP;
    }

    public void SetMaxMP()
    {
        currentMP = maxMP;
    }

    public void AddPlayerEXP(int EXPAmount)
    {
        currentEXP += EXPAmount;
    }

    public float GetMaxMP()
    {
        return maxMP;
    }

    public float GetCurrentMP()
    {
        return currentMP;
    }
}
