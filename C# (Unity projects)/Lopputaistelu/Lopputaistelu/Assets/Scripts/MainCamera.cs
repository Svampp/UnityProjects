using System.Collections;
using UnityEngine;

/// <summary>
/// Controls the main camera's movement and ensures it stays within defined boundaries.
/// </summary>
public class MainCamera : MonoBehaviour
{
    private Transform target; // The target the camera follows (usually the player).
    private float topLeftX; // Left boundary of the camera's movement.
    private float topLeftY; // Top boundary of the camera's movement.
    private float bottomRightX; // Right boundary of the camera's movement.
    private float bottomRightY; // Bottom boundary of the camera's movement.

    /// <summary>
    /// Initializes the camera's target to the player.
    /// </summary>
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Updates the camera's position, ensuring it stays within defined bounds.
    /// </summary>
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, topLeftX, bottomRightX), // Constrain X position.
            Mathf.Clamp(target.position.y, bottomRightY, topLeftY), // Constrain Y position.
            transform.position.z // Maintain current Z position.
        );
    }

    /// <summary>
    /// Sets the camera's movement bounds based on the dimensions of the map.
    /// </summary>
    /// <param name="map">The map GameObject that defines the boundaries.</param>
    public void SetBound(GameObject map)
    {
        // Get the map configuration and camera parameters.
        SuperTiled2Unity.SuperMap config = map.GetComponent<SuperTiled2Unity.SuperMap>();
        float cameraSize = Camera.main.orthographicSize; // Half the vertical size of the camera.
        float aspectRatio = Camera.main.aspect * cameraSize; // Half the horizontal size of the camera.

        // Calculate the boundaries for camera movement.
        topLeftX = map.transform.position.x + aspectRatio;
        topLeftY = map.transform.position.y - cameraSize;
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio;
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize;
    }
}
