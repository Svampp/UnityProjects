using UnityEngine;

/// <summary>
/// Controls the rotation of a turret when the space key is pressed.
/// </summary>
public class TurretController : MonoBehaviour
{
    // The speed at which the turret rotates, measured in degrees per second.
    public float rotationSpeed = 100f;

    void Update()
    {
        // Check if the space key is being held down.
        if (Input.GetKey(KeyCode.Space))
        {
            // Rotate the turret around the Y-axis at the specified speed.
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
