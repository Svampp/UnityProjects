using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    // Target to follow (usually the player)
    private Transform target;

    // Boundaries of the camera's movement within the map
    private float topLeftX;
    private float topLeftY;
    private float bottomRightX;
    private float bottomRightY;

    private void Awake()
    {
        // Find and set the target as the GameObject tagged "Player"
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        // Clamp the camera's position to stay within the defined boundaries
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, topLeftX, bottomRightX), // X position
            Mathf.Clamp(target.position.y, bottomRightY, topLeftY), // Y position
            transform.position.z); // Z position remains unchanged
    }

    public void SetBound(GameObject map)
    {
        // Get the map configuration from the SuperTiled2Unity component
        SuperTiled2Unity.SuperMap config = map.GetComponent<SuperTiled2Unity.SuperMap>();

        // Retrieve the camera's orthographic size (half the height of the camera view)
        float cameraSize = Camera.main.orthographicSize;

        // Calculate the aspect ratio width (half the width of the camera view)
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Calculate the top-left and bottom-right bounds of the camera within the map
        topLeftX = map.transform.position.x + aspectRatio;
        topLeftY = map.transform.position.y - cameraSize;
        bottomRightX = map.transform.position.x + config.m_Width - aspectRatio;
        bottomRightY = map.transform.position.y - config.m_Height + cameraSize;
    }
}
