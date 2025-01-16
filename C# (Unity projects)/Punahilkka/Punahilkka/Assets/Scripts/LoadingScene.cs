using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls loading of a new scene after a specified wait time.
/// </summary>
public class LoadingScene : MonoBehaviour
{
    public float waitToLoad; // Time to wait before loading the new scene

    /// <summary>
    /// Counts down the timer and loads the next scene when time reaches zero.
    /// </summary>
    private void Update()
    {
        if (waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime; // Decrease the wait time

            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene("SampleScene"); // Load the specified scene
            }
        }
    }
}
