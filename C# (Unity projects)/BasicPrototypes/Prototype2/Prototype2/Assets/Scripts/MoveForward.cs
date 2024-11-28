using UnityEngine;

// This script moves a GameObject forward continuously at a specified speed.
public class MoveForward : MonoBehaviour
{
    // Speed at which the GameObject moves forward.
    public float speed = 40.0f;
    void Update()
    {
        // Move the GameObject forward along its local z-axis.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
