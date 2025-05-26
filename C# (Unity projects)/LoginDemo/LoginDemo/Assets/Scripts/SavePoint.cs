/// <summary>
/// Represents a save point in the game that updates the player's position in the database when triggered.
/// </summary>
public class SavePoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player.
        if (other.CompareTag("Player"))
        {
            // Update the player's position in the database.
            SaveSystem.UpdatePlayer(other.GetComponent<Player>());

            // Destroy the save point to prevent repeated use.
            Destroy(gameObject);
        }
    }
}
