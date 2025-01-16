using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script handles the in-game pause menu, allowing the player to pause, resume,
/// or return to the main menu. It also manages the time scale during pause.
/// </summary>
public class Menu : MonoBehaviour
{
    // Indicates whether the game is currently paused
    public static bool GameIsPaused = false;

    // Reference to the pause menu UI
    public GameObject pauseMenuUI;

    // Name of the main menu scene
    public string mainMenu;

    /// <summary>
    /// Initializes the pause menu by hiding it at the start.
    /// </summary>
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    /// <summary>
    /// Checks for pause and resume inputs from the player each frame.
    /// </summary>
    private void Update()
    {
        // Pauses the game when "R" is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameIsPaused = true;
            Pause();
        }

        // Toggles between pause and resume when "Escape" is pressed
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
    /// Resumes the game by hiding the pause menu and resetting the time scale.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pauses the game by showing the pause menu and freezing the time scale.
    /// </summary>
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /// <summary>
    /// Returns to the main menu scene when the respective button is clicked.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
