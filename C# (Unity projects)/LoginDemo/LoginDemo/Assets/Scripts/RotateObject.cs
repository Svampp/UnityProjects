/// <summary>
/// Rotates an object around a specified axis at a specified speed.
/// </summary>
public class RotateObject : MonoBehaviour
{
    [SerializeField] int rotateSpeed; // Speed of rotation.
    [SerializeField] Vector3 rotateAxis; // Axis around which the object rotates.

    void Update()
    {
        // Rotate the object around the specified axis in world space.
        transform.Rotate(rotateSpeed * Time.deltaTime * rotateAxis, Space.World);
    }
}
