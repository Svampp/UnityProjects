/// <summary>
/// Represents a key that can be picked up and added to the player's inventory.
/// </summary>
public class KeyInteractable : MonoBehaviour, IInteractable
{
    // The text displayed to the player when interacting with the key.
    [SerializeField] string interactText;

    public string GetInteractText()
    {
        return interactText;
    }

    /// <summary>
    /// Handles the interaction logic when the key is picked up.
    /// </summary>
    public void Interact()
    {
        Debug.Log("Avain löydetty. Mene tyrmään!"); // "Key found. Go to the dungeon!" in Finnish.

        // Add the key to the player's inventory.
        InventoryManager.Instance.Add(GetComponent<PickupItem>());

        StopInteract(); // Disable further interaction.
        Destroy(gameObject); // Remove the key from the scene.
    }

    public void StopInteract()
    {
        GetComponent<Collider>().enabled = false; // Disable the collider.
    }
}
