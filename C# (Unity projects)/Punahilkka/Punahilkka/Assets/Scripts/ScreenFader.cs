using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages screen fade transitions between scenes or gameplay events.
/// </summary>
public class ScreenFader : MonoBehaviour
{
    private Animator anim; // Animator controlling fade transitions
    public bool IsFading { get; set; } // Indicates if a fade is in progress

    /// <summary>
    /// Retrieves the Animator component at the start.
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Triggers a fade-to-white transition.
    /// </summary>
    public IEnumerator FadeToWhite()
    {
        IsFading = true;
        anim.SetTrigger("FadeToWhite");
        while (IsFading)
        {
            yield return null; // Wait until the fade completes
        }
    }

    /// <summary>
    /// Triggers a fade-to-black transition.
    /// </summary>
    public IEnumerator FadeToBlack()
    {
        IsFading = true;
        anim.SetTrigger("FadeToBlack");
        while (IsFading)
        {
            yield return null; // Wait until the fade completes
        }
    }

    /// <summary>
    /// Called by the animation system to signal fade completion.
    /// </summary>
    public void FadeAnimationComplete() => IsFading = false;
}
