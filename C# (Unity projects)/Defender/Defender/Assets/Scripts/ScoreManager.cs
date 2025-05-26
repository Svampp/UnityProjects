using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Text fields to display HP and score information in the UI
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text scoreText;

    // Variable to store the high score value
    private int saveHighScore = 0;

    // The player's current score
    private int score;

    // Property to get and set the current score
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            // Update the score text to display current score and high score
            scoreText.text = $"Pisteet: {score} / {saveHighScore}";
        }
    }

    // The player's current health points (HP)
    private int hp = 3;

    // Property to get and set the player's health
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;

            // If health reaches 0 or below, update the high score and restart the scene
            if (hp <= 0)
            {
                UpdateHighScore(Score); // Update the high score before restarting
                SceneManager.LoadScene(0); // Restart the game (load the first scene)
            }
            else
            {
                // Update the HP display if the player is still alive
                hpText.text = $"HP: {hp}";
            }
        }
    }

    private void Start()
    {
        // Load the saved high score from PlayerPrefs
        saveHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Initialize the UI with the current score and high score
        scoreText.text = $"Pisteet: {Score} / {saveHighScore}";
        hpText.text = $"HP: {HP}";
    }

    private void UpdateHighScore(int currentScore)
    {
        // Retrieve the currently saved high score
        saveHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // If the current score exceeds the saved high score, update the high score
        if (currentScore > saveHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore); // Save the new high score
            PlayerPrefs.Save(); // Ensure the high score is saved to disk

            // Update the score display to show the new high score
            scoreText.text = $"Pisteet: {Score} / {currentScore}";
        }
    }
}
