using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the transition to the end scene after a delay.
/// </summary>
public class EndSceneLoading : MonoBehaviour
{
    /// <summary>
    /// Starts the coroutine to load the end scene after a delay.
    /// </summary>
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    /// <summary>
    /// Waits for a specified delay before loading the "End" scene.
    /// </summary>
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds.
        SceneManager.LoadScene("End"); // Load the end scene.
    }
}
