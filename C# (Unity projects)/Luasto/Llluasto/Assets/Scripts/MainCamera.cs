using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Target the camera will follow (Player)
    private Transform target;

    // Boundaries for the camera (top-left and bottom-right coordinates)
    private float topLeftX;
    private float topLeftY;
    private float bottomRightX;
    private float bottomRightY;

    // Called when the script instance is being loaded
    private void Awake()
    {
        // Find the player object by its tag and store its transform
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // LateUpdate is called after all Update methods have been called
    private void LateUpdate()
    {
        // Update camera position to follow the player, clamped within the defined boundaries
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, topLeftX, bottomRightX), // Clamp horizontal position
            Mathf.Clamp(target.position.y, bottomRightY, topLeftY), // Clamp vertical position
            transform.position.z); // Keep the camera's z position fixed
    }

    // Set the camera's bounds based on the map's size
    public void SetBound(GameObject map)
    {
        // Get the SuperMap component from the map object to access its size
        SuperTiled2Unity.SuperMap config = map.GetComponent<SuperTiled2Unity.SuperMap>();

        // Get the camera's orthographic size and aspect ratio
        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Set the camera's top-left and bottom-right boundaries based on map size
        topLeftX = map.transform.position.x + aspectRatio; // Right boundary of the camera's view
        topLeftY = map.transform.position.y - cameraSize; // Top boundary of the camera's view
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio; // Left boundary
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize; // Bottom boundary
    }
}
