using UnityEngine;

// This script changes the material color of a GameObject to dark green when the game starts.
public class ChangeToDarkGreen : MonoBehaviour
{
    void Start()
    {
        // Get the Renderer component attached to this GameObject.
        Renderer renderer = GetComponent<Renderer>();

        // Set the material's color to dark green.
        renderer.material.SetColor("_Color", new Color(0.0f, 0.5f, 0.0f, 1.0f));
    }
}
