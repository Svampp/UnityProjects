using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the pause menu functionality, including pausing and resuming the game.
/// </summary>
public class Menu : MonoBehaviour
{
    // Tracks whether the game is paused.
    public static bool GameIsPaused = false;

    // Reference to the UI object for the pause menu.
    public GameObject pauseMenuUI;

    /// <summary>
    /// Initializes the pause menu UI state.
    /// </summary>
    private void Start()
    {
        // Ensure the pause menu is hidden at the start.
        pauseMenuUI.SetActive(false);
    }

    /// <summary>
    /// Checks for input to pause or resume the game.
    /// </summary>
    private void Update()
    {
        // If the "R" key is pressed, pause the game.
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameIsPaused = true;
            Pause();
        }

        // If the "Escape" key is pressed, toggle between pause and resume states.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Resumes the game by hiding the pause menu and restoring time scale.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f; // Resume normal time progression.
        GameIsPaused = false;
    }

    /// <summary>
    /// Pauses the game by displaying the pause menu and stopping time progression.
    /// </summary>
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Stop time progression.
        GameIsPaused = true;
    }
}
