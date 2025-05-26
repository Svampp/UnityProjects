using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the splash screen behavior, including logo fade-in and transition to the main menu.
/// </summary>
public class SplashScreen : MonoBehaviour
{
    public Image logo; // The logo displayed on the splash screen.
    private Color logoColor; // The current color of the logo (used for fading).

    public float lerpMultiplier = 0.02f; // Speed of the fade-in effect.
    private int timeToWait = 6; // Time to wait before transitioning to the main menu.

    /// <summary>
    /// Initializes the splash screen and starts the transition coroutine.
    /// </summary>
    private void Start()
    {
        logoColor = new Color(1, 1, 1, 0); // Start with a transparent logo.
        logo.color = logoColor;

        StartCoroutine(GotoMainMenuCo()); // Start the transition to the main menu.
    }

    /// <summary>
    /// Gradually fades the logo in over time.
    /// </summary>
    private void Update()
    {
        logoColor = Color.Lerp(logoColor, new Color(1, 1, 1, 1), Time.deltaTime * lerpMultiplier); // Interpolate towards opaque.
        logo.color = logoColor; // Apply the updated color to the logo.
    }

    /// <summary>
    /// Waits for a specified duration before loading the main menu scene.
    /// </summary>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    private IEnumerator GotoMainMenuCo()
    {
        yield return new WaitForSeconds(timeToWait); // Wait for the specified duration.
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene.
    }
}
