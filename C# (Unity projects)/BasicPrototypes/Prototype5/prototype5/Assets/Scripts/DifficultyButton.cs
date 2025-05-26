using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Reference to the Button component
    private Button button;

    // Reference to the GameManager to communicate game state
    private GameManager gameManager;

    // Difficulty level associated with this button
    public int difficulty;

    void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // Find the GameManager object in the scene and get its GameManager component
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Add a listener to the button's onClick event to call SetDifficulty when clicked
        button.onClick.AddListener(SetDifficulty);
    }

    // Called when the button is clicked
    void SetDifficulty()
    {
        // Log the name of the button that was clicked for debugging purposes
        Debug.Log(gameObject.name + " was clicked");

        // Start the game with the difficulty level associated with this button
        gameManager.StartGame(difficulty);
    }
}
