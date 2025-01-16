using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health of enemies, including taking damage, updating the health bar,
/// and handling death logic.
/// </summary>
public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private string enemyName; // Name of the enemy
    [SerializeField]
    private float EnemyMaxHP = 100f; // Maximum health of the enemy
    [SerializeField]
    private float enemyCurrentHP; // Current health of the enemy

    public Image enemyHealthbar; // UI element representing the health bar
    public float lerpSpeed; // Speed of the health bar interpolation

    /// <summary>
    /// Initializes the enemy's current health to the maximum value.
    /// </summary>
    private void Start()
    {
        enemyCurrentHP = EnemyMaxHP; // Set initial health
    }

    /// <summary>
    /// Updates the health bar and allows manual damage testing with the "J" key.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Test damage input
        {
            HurtEnemy(5); // Deal 5 damage to the enemy
        }

        CheckEnemyStatus(); // Update the health bar to reflect current health
    }

    /// <summary>
    /// Updates the health bar to visually represent the current health.
    /// </summary>
    private void CheckEnemyStatus()
    {
        if (enemyCurrentHP != enemyHealthbar.fillAmount)
        {
            enemyHealthbar.fillAmount = Mathf.Lerp(
                enemyHealthbar.fillAmount,
                enemyCurrentHP / EnemyMaxHP,
                Time.deltaTime * lerpSpeed); // Smooth transition of the health bar
        }
    }

    /// <summary>
    /// Reduces the enemy's health and checks for death.
    /// </summary>
    /// <param name="damageToTake">Amount of damage to apply.</param>
    public void HurtEnemy(int damageToTake)
    {
        enemyCurrentHP -= damageToTake; // Reduce current health
        AudioManager.instance.Play("HurtEnemy"); // Play damage sound effect

        if (enemyCurrentHP <= 0) // Check if the enemy is dead
        {
            enemyCurrentHP = 0;
            Die(); // Trigger death logic
        }
    }

    /// <summary>
    /// Handles the death of the enemy, including destruction and playing effects.
    /// </summary>
    private void Die()
    {
        print(enemyName + " die"); // Debug message for enemy death
        Destroy(gameObject, 0.5f); // Destroy the enemy object after a delay
        AudioManager.instance.Play("EnemyDie"); // Play death sound effect
    }
}
