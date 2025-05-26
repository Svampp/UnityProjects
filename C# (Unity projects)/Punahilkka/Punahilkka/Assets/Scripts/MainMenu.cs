using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the main menu of the game, including starting a new game,
/// continuing a saved game, or exiting the application.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Name of the scene to load for a new game
    public string newGameScene;

    // Name of the scene to load for continuing a saved game
    public string loadGameScene;

    // Reference to the continue button in the UI
    public GameObject continueButton;

    /// <summary>
    /// Loads the saved game scene when the "Continue" button is clicked.
    /// </summary>
    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    /// <summary>
    /// Starts a new game by loading the specified scene.
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    /// <summary>
    /// Exits the application when the "Exit" button is clicked.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
