using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health of enemies, including taking damage, updating the health bar,
/// and handling enemy death.
/// </summary>
public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private string enemyName; // Name of the enemy for identification.
    [SerializeField]
    private float EnemyMaxHP = 100f; // Maximum health points of the enemy.
    [SerializeField]
    public float enemyCurrentHP; // Current health points of the enemy.

    public Image enemyHealthbar; // UI element representing the enemy's health bar.
    public float lerpSpeed; // Speed of health bar interpolation.

    /// <summary>
    /// Initializes the enemy's current health to the maximum value.
    /// </summary>
    private void Start()
    {
        enemyCurrentHP = EnemyMaxHP;
    }

    /// <summary>
    /// Updates the health bar and checks the enemy's status.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Debug feature: Test damage on spacebar press.
        {
            HurtEnemy(0);
        }

        CheckEnemyStatus();
    }

    /// <summary>
    /// Smoothly updates the health bar to reflect the enemy's current health.
    /// </summary>
    private void CheckEnemyStatus()
    {
        if (enemyCurrentHP != enemyHealthbar.fillAmount)
        {
            enemyHealthbar.fillAmount = Mathf.Lerp(enemyHealthbar.fillAmount,
                enemyCurrentHP / EnemyMaxHP, Time.deltaTime * lerpSpeed);
        }
    }

    /// <summary>
    /// Reduces the enemy's health by a specified amount and handles death if health reaches zero.
    /// </summary>
    /// <param name="damageToTake">Amount of damage to subtract from the enemy's health.</param>
    public void HurtEnemy(int damageToTake)
    {
        enemyCurrentHP -= damageToTake;
        AudioManager.instance.Play("HurtEnemy"); // Play sound effect for taking damage.
        if (enemyCurrentHP <= 0)
        {
            enemyCurrentHP = 0;
            Die(); // Trigger death sequence if health is zero or below.
        }
    }

    /// <summary>
    /// Handles the enemy's death, including notifying other systems and destroying the enemy object.
    /// </summary>
    public void Die()
    {
        FindObjectOfType<TimerManager>().EnemyDied(); // Notify the timer manager of the enemy's death.
        Destroy(gameObject, 0.5f); // Destroy the enemy object after a short delay.
    }
}
