using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    // Enemy's name (optional for UI or logging purposes)
    [SerializeField]
    private string enemyName;

    // Enemy's maximum health points
    [SerializeField]
    private float EnemyMaxHP = 100f;

    // Enemy's current health points
    [SerializeField]
    private float enemyCurrentHP;

    // UI element to display the enemy's health bar
    public Image enemyHealthbar;

    // Speed at which the health bar updates visually
    public float lerpSpeed;

    // Whether the enemy is currently targeted by the player
    private bool isTargeted;

    // Optional: GameObject to activate upon the enemy's death
    public GameObject objectToActivate;

    private void Start()
    {
        // Initialize the enemy's current health to the maximum health
        enemyCurrentHP = EnemyMaxHP;
    }

    void Update()
    {
        // If the enemy is targeted and the player presses the space key, apply damage
        if (isTargeted && Input.GetKeyDown(KeyCode.Space))
        {
            HurtEnemy(20); // Apply 20 damage (can be replaced with actual attack logic)
        }

        // Update the enemy's health bar to reflect the current health
        CheckEnemyStatus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Set the enemy as targeted when the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            isTargeted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Remove the targeting status when the player leaves the trigger area
        if (collision.CompareTag("Player"))
        {
            isTargeted = false;
        }
    }

    private void CheckEnemyStatus()
    {
        // Smoothly update the health bar's fill amount to match the enemy's current health
        if (enemyCurrentHP != enemyHealthbar.fillAmount)
        {
            enemyHealthbar.fillAmount = Mathf.Lerp(enemyHealthbar.fillAmount,
                enemyCurrentHP / EnemyMaxHP, Time.deltaTime * lerpSpeed);
        }
    }

    public void HurtEnemy(int damageToTake)
    {
        // Reduce the enemy's health by the damage amount
        enemyCurrentHP -= damageToTake;

        // Play enemy hurt sound (optional, AudioManager setup required)
        // AudioManager.instance.Play("HurtEnemy");

        // Check if the enemy's health has dropped to zero or below
        if (enemyCurrentHP <= 0)
        {
            enemyCurrentHP = 0;
            Die(); // Trigger the death behavior

            // Activate an associated GameObject (e.g., loot drop or quest objective)
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }

    private void Die()
    {
        // Play enemy death sound (optional, AudioManager setup required)
        // AudioManager.instance.Play("EnemyDie");

        // Destroy the enemy GameObject after a slight delay
        Destroy(gameObject, 0.5f);
    }
}
