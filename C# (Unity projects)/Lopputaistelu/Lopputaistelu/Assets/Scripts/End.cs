using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles the end sequence of the game by displaying text messages sequentially
/// and quitting the application after a delay.
/// </summary>
public class End : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // Array of TextMeshPro elements for the end sequence.
    private int currentTextIndex = 0; // Index of the current text to display.

    /// <summary>
    /// Starts the end sequence coroutine.
    /// </summary>
    void Start()
    {
        StartCoroutine(ShowTextsWithDelay());
    }

    /// <summary>
    /// Displays text messages one by one with a delay and quits the application.
    /// </summary>
    private IEnumerator ShowTextsWithDelay()
    {
        while (currentTextIndex < texts.Length)
        {
            texts[currentTextIndex].gameObject.SetActive(true); // Show the current text.
            yield return new WaitForSeconds(2f); // Wait for 2 seconds.
            texts[currentTextIndex].gameObject.SetActive(false); // Hide the text.
            currentTextIndex++; // Move to the next text.
        }

        yield return new WaitForSeconds(4f); // Wait 4 seconds before quitting.
        Application.Quit(); // Quit the application.
    }
}
