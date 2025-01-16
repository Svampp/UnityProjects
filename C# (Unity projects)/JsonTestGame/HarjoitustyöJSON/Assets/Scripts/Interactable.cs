using UnityEngine;

/// <summary>
/// A base class for objects that can be interacted with and collected.
/// </summary>
public class Interactable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        // Add the item to the inventory.
        InventoryManager.Instance.Add(GetComponent<PickupItem>());

        // Stop further interaction.
        StopInteract();

        // Destroy the object after interaction.
        Destroy(gameObject);
    }

    public void StopInteract()
    {
        // Disable the collider to prevent further interactions.
        GetComponent<Collider>().enabled = false;
    }
}
