/// <summary>
/// Controls the player's movement and rotation based on input.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Speed for moving forward or backward.
    float speed = 20.0f;

    // Speed for rotating the player.
    float turnSpeed = 45.0f;

    // Input values for horizontal and vertical axes.
    float horizontalInput;
    float forwardInput;

    void Update()
    {
        // Get player input from keyboard or controller.
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the player forward/backward and rotate them based on input.
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
