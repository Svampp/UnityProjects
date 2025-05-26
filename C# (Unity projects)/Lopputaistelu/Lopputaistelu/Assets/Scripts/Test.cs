using System.Collections;
using UnityEngine;

/// <summary>
/// A simple script for testing collision detection.
/// Logs the name of any object that enters the trigger collider.
/// </summary>
public class Test : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// Logs the name of the entering object.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Collider entered: {other.gameObject.name}"); // Log the name of the object.
    }
}
