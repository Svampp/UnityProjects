using System.Collections;
using UnityEngine;

/// <summary>
/// Handles screen fade transitions using an Animator component.
/// </summary>
public class ScreenFader : MonoBehaviour
{
    private Animator anim; // Reference to the Animator component for fade animations.
    public bool IsFading { get; set; } // Indicates if a fade animation is in progress.

    /// <summary>
    /// Initializes the Animator component.
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Starts a fade-to-white transition.
    /// </summary>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    public IEnumerator FadeToWhite()
    {
        IsFading = true;
        anim.SetTrigger("FadeToWhite"); // Trigger the fade-to-white animation.

        // Wait until the fade animation completes.
        while (IsFading)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Starts a fade-to-black transition.
    /// </summary>
    /// <returns>An IEnumerator for coroutine handling.</returns>
    public IEnumerator FadeToBlack()
    {
        IsFading = true;
        anim.SetTrigger("FadeToBlack"); // Trigger the fade-to-black animation.

        // Wait until the fade animation completes.
        while (IsFading)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Called by animation events to mark the end of a fade animation.
    /// </summary>
    public void FadeAnimationComplete()
    {
        IsFading = false;
    }
}
