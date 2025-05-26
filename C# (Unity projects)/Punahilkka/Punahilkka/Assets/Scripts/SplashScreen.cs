using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the splash screen behavior, including fading in the logo
/// and transitioning to the main menu.
/// </summary>
public class SplashScreen : MonoBehaviour
{
    public Image logo; // UI Image for the logo
    private Color logoColor; // Current color of the logo

    public float lerpMultiplier = 0.02f; // Speed of the fade-in effect
    private int timeToWait = 6; // Time to wait before transitioning to the main menu

    /// <summary>
    /// Initializes the splash screen by setting the logo color and starting the coroutine.
    /// </summary>
    private void Start()
    {
        logoColor = new Color(1, 1, 1, 0); // Set logo to transparent
        logo.color = logoColor;

        StartCoroutine(GotoMainMenuCo()); // Start the main menu transition coroutine
    }

    /// <summary>
    /// Fades in the logo and transitions to the main menu after a delay.
    /// </summary>
    IEnumerator GotoMainMenuCo()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    /// <summary>
    /// Updates the logo color to create a fade-in effect.
    /// </summary>
    private void Update()
    {
        logoColor = Color.Lerp(logoColor, new Color(1, 1, 1, 1), Time.time * lerpMultiplier);
        logo.color = logoColor;
    }
}
