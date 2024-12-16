using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private string enemyName;  // Name of the enemy for identification (optional)
    [SerializeField]
    private float EnemyMaxHP = 100f;  // Maximum health of the enemy
    [SerializeField]
    private float enemyCurrentHP;  // Current health of the enemy

    public Image enemyHealthbar;  // Health bar UI to reflect enemy's health
    public float lerpSpeed;  // Speed at which the health bar updates

    private bool isTargeted;  // Flag to track if the player is targeting the enemy

    private void Start()
    {
        enemyCurrentHP = EnemyMaxHP;  // Initialize the enemy's current health to max health
    }

    void Update()
    {
        // If the enemy is targeted and the player presses space, the enemy takes damage
        if (isTargeted && Input.GetKeyDown(KeyCode.Space))
        {
            HurtEnemy(20);  // Apply 20 damage to the enemy
        }

        // Update the enemy's health bar UI
        CheckEnemyStatus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When the player enters the enemy's trigger zone, start targeting
        if (collision.CompareTag("Player"))
        {
            isTargeted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // When the player exits the trigger zone, stop targeting
        if (collision.CompareTag("Player"))
        {
            isTargeted = false;
        }
    }

    // Smoothly update the enemy's health bar UI based on current health
    private void CheckEnemyStatus()
    {
        if (enemyCurrentHP != enemyHealthbar.fillAmount)
        {
            enemyHealthbar.fillAmount = Mathf.Lerp(enemyHealthbar.fillAmount,
                enemyCurrentHP / EnemyMaxHP, Time.deltaTime * lerpSpeed);
        }
    }

    // Method to apply damage to the enemy
    public void HurtEnemy(int damageToTake)
    {
        enemyCurrentHP -= damageToTake;  // Reduce enemy health by damage amount
        //AudioManager.instance.Play("HurtEnemy");  // Optional: Play hurt sound

        // If the enemy's health reaches zero, call the Die method
        if (enemyCurrentHP <= 0)
        {
            enemyCurrentHP = 0;
            Die();
        }
    }

    // Method to handle enemy's death (destroy the enemy object)
    private void Die()
    {
        Destroy(gameObject, 0.5f);  // Destroy the enemy after a short delay
        //AudioManager.instance.Play("EnemyDie");  // Optional: Play death sound
    }
}
