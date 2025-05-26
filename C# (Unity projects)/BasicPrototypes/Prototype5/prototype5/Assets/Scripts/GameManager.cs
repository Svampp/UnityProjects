using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // List of possible target prefabs to spawn
    public List<GameObject> targets;

    // UI elements for displaying the score and game over text
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    // UI button for restarting the game
    public Button restartButton;

    // Title screen object to hide once the game starts
    public GameObject titleScreen;

    // Boolean to track if the game is active
    public bool isGameActive;

    // Player's current score
    private int score;

    // Rate at which targets will spawn
    private float spawnRate = 1.0f;

    // Coroutine to spawn targets at regular intervals
    IEnumerator SpawnTarget()
    {
        // Continue spawning targets while the game is active
        while (isGameActive)
        {
            // Wait for the specified spawn rate before spawning a new target
            yield return new WaitForSeconds(spawnRate);

            // Choose a random target from the list
            int index = Random.Range(0, targets.Count);

            // Instantiate the chosen target at its default position and rotation
            Instantiate(targets[index]);
        }
    }

    // Updates the player's score and updates the score text on the UI
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd; // Add the specified amount to the score
        scoreText.text = "Score: " + score; // Update the UI text
    }

    // Ends the game, displays the game over text, and shows the restart button
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); // Show the restart button
        gameOverText.gameObject.SetActive(true); // Show the game over text
        isGameActive = false; // Set the game state to inactive
    }

    // Restarts the game by reloading the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    // Starts the game, sets the difficulty, and initializes the game state
    public void StartGame(int difficulty)
    {
        isGameActive = true; // Set the game state to active
        score = 0; // Reset the score to 0
        spawnRate /= difficulty; // Adjust the spawn rate based on difficulty

        StartCoroutine(SpawnTarget()); // Start spawning targets
        UpdateScore(0); // Update the score display to show 0

        titleScreen.gameObject.SetActive(false); // Hide the title screen
    }
}
