using TMPro;
using UnityEngine;

// This script manages the high score in a game and updates the UI to display the current high score.
public class ScoreManager : MonoBehaviour
{
    // Static instance to make the ScoreManager easily accessible from other scripts.
    public static ScoreManager instance;
    // Reference to the TextMeshPro text element for displaying the high score.
    [SerializeField] TMP_Text highScoreText;

    private void Start()
    {
        // Initialize the static instance for global access.
        instance = this;
        // Retrieve the saved high score from PlayerPrefs (default to 0 if not found).
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update the high score text in the UI.
        highScoreText.text = $"Paras tulos: {savedHighScore}";
    }

    // Updates the high score if the current score exceeds the saved high score.
    public void UpdateHighScore(int currentScore)
    {
        // Retrieve the saved high score from PlayerPrefs.
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Check if the current score is greater than the saved high score.
        if (currentScore > savedHighScore)
        {
            // Save the new high score in PlayerPrefs.
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();

            // Update the high score text in the UI.
            highScoreText.text = $"Paras tulos: {currentScore}";
        }
    }
}
