using UnityEngine;

/// <summary>
/// Represents a potion that the player can interact with and add to their inventory.
/// </summary>
public class PotionInteractable : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Provides the text displayed to the player when interacting with the potion.
    /// </summary>
    /// <returns>A string prompting the player to pick up the potion.</returns>
    public string GetInteractText()
    {
        return "Taikajuoma - Ota minut!"; // "Magic Potion - Take me!" in Finnish.
    }

    /// <summary>
    /// Handles the interaction logic when the potion is picked up.
    /// </summary>
    public void Interact()
    {
        // Log a message indicating the potion was picked up.
        Debug.Log("Taikajuoma löydetty!"); // "Magic potion found!" in Finnish.

        // Add the potion to the player's inventory.
        InventoryManager.Instance.Add(GetComponent<PickupItem>());

        // Stop further interaction and destroy the potion.
        StopInteract();
        Destroy(gameObject);
    }

    /// <summary>
    /// Disables the potion's collider to prevent further interaction.
    /// </summary>
    public void StopInteract()
    {
        GetComponent<Collider>().enabled = false;
    }
}
