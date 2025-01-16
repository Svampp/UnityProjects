/// <summary>
/// Updates the inventory display in the UI when the inventory changes.
/// </summary>
public class PlayerInventoryDisplay : MonoBehaviour
{
    // Text field to display the inventory contents.
    [SerializeField] TMP_Text inventorytext;

    public void OnChangeInventory(Dictionary<PickupItem.PickupType, int> inventory)
    {
        // Clear the existing text.
        inventorytext.text = "";

        // Create a string to represent the new inventory.
        string newInventoryText = "Inventory: ";

        // Add each item and its count to the display text.
        foreach (var item in inventory)
        {
            int itemTotal = item.Value;
            string description = item.Key.ToString();
            newInventoryText += $"{description}:{itemTotal} | ";
        }

        // Show a placeholder if the inventory is empty.
        if (inventory.Count < 1)
        {
            newInventoryText = "(empty inventory)";
        }

        // Update the UI text.
        inventorytext.text = newInventoryText;
    }
}
