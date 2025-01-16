/// <summary>
/// Makes the camera follow the player with a fixed offset.
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    // The player GameObject to follow.
    public GameObject player;

    // The offset position relative to the player.
    Vector3 offset = new Vector3(0, 8, -6);

    void LateUpdate()
    {
        // Update the camera's position to follow the player while maintaining the offset.
        transform.position = player.transform.position + offset;
    }
}
