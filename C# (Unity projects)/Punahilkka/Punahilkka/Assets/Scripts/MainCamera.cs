using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the camera's position to follow the player while staying within bounds.
/// </summary>
public class MainCamera : MonoBehaviour
{
    private Transform target; // The player to follow
    private float topLeftX; // Top-left boundary X position
    private float topLeftY; // Top-left boundary Y position
    private float bottomRightX; // Bottom-right boundary X position
    private float bottomRightY; // Bottom-right boundary Y position

    /// <summary>
    /// Finds the player object to set as the camera's target.
    /// </summary>
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Updates the camera's position to follow the player while staying within bounds.
    /// </summary>
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, topLeftX, bottomRightX),
            Mathf.Clamp(target.position.y, bottomRightY, topLeftY),
            transform.position.z);
    }

    /// <summary>
    /// Sets the camera's movement bounds based on the map configuration.
    /// </summary>
    /// <param name="map">The map object defining the boundaries.</param>
    public void SetBound(GameObject map)
    {
        SuperTiled2Unity.SuperMap config = map.GetComponent<SuperTiled2Unity.SuperMap>();
        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;
        topLeftX = map.transform.position.x + aspectRatio;
        topLeftY = map.transform.position.y - cameraSize;
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio;
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize;
    }
}
