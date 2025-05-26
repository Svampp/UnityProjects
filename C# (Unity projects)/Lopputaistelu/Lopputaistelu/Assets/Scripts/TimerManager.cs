using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the battle timer and determines the outcome of battles.
/// Handles player and enemy deaths, as well as time-based battle conclusions.
/// </summary>
public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText; // UI text element to display the timer.
    public GameObject player; // Reference to the player object.
    public GameObject enemy; // Reference to the enemy object.
    public float battleDuration = 30f; // Duration of the battle in seconds.

    private float currentTime; // Tracks the remaining battle time.
    private bool timerActive = false; // Indicates whether the timer is active.
    private bool battleEnded = false; // Indicates whether the battle has concluded.

    /// <summary>
    /// Hides the timer text at the start of the game.
    /// </summary>
    void Start()
    {
        timerText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the timer during an active battle and handles time expiration.
    /// </summary>
    void Update()
    {
        if (timerActive && !battleEnded)
        {
            currentTime -= Time.deltaTime; // Decrease the timer.
            timerText.text = currentTime.ToString("F2"); // Display the remaining time.

            if (currentTime <= 0)
            {
                EndBattle(); // Handle battle timeout.
            }
        }
    }

    /// <summary>
    /// Starts the battle timer and displays the timer UI.
    /// </summary>
    public void StartBattle()
    {
        timerText.gameObject.SetActive(true);
        currentTime = battleDuration;
        timerActive = true;
    }

    /// <summary>
    /// Handles the player's death, transitioning to the appropriate scene.
    /// </summary>
    public void PlayerDied()
    {
        if (!battleEnded)
        {
            battleEnded = true;
            SceneManager.LoadScene("WolfWon"); // Load the scene for enemy victory.
            AudioManager.instance.StopPlay("Back");
            AudioManager.instance.Play("Laugh"); // Play enemy victory sound.
        }
    }

    /// <summary>
    /// Handles the enemy's death, transitioning to the appropriate scene.
    /// </summary>
    public void EnemyDied()
    {
        if (!battleEnded)
        {
            battleEnded = true;
            SceneManager.LoadScene("PunahilkkaWon"); // Load the scene for player victory.
            AudioManager.instance.StopPlay("Back");
            AudioManager.instance.Play("Win"); // Play player victory sound.
        }
    }

    /// <summary>
    /// Handles the end of the battle due to time expiration or other conditions.
    /// </summary>
    private void EndBattle()
    {
        battleEnded = true;
        SceneManager.LoadScene("NoWinner"); // Load the scene for no winner.
        AudioManager.instance.StopPlay("Back");
        AudioManager.instance.Play("NoWin"); // Play no winner sound.
    }
}
