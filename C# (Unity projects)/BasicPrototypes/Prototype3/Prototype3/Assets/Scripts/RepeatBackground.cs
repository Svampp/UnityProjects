using UnityEngine;

// This script creates an infinite scrolling background effect by resetting the background's position once it has moved a certain distance.
public class RepeatBackground : MonoBehaviour
{
    // The starting position of the background.
    private Vector3 startPos;
    // Half the width of the background, used to determine when to reset its position.
    private float repeatWidth;

    void Start()
    {
        // Store the initial position of the background.
        startPos = transform.position;
        // Calculate half the width of the background.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    void Update()
    {
        // Check if the background has moved beyond its reset point.
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Reset the background's position to its starting point.
            transform.position = startPos;
        }
    }
}
