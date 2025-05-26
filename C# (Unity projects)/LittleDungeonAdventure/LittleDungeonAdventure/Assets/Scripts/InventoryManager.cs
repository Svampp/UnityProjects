/// <summary>
/// Manages the player's inventory, including adding items and saving/loading data.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Singleton instance of the InventoryManager.
    public static InventoryManager Instance { get; private set; }

    // Dictionary to store the items in the inventory.
    Dictionary<PickupItem.PickupType, int> items = new Dictionary<PickupItem.PickupType, int>();

    // Reference to the UI display for the inventory.
    PlayerInventoryDisplay playerInventoryDisplay;

    void Awake()
    {
        // Initialize the singleton instance.
        Instance = this;

        // Get the inventory display component.
        playerInventoryDisplay = GetComponent<PlayerInventoryDisplay>();
        playerInventoryDisplay.OnChangeInventory(items);
    }

    void Start()
    {
        // Load the inventory from saved data.
        items = DataManager.LoadInventory();

        // Update the UI or initialize an empty inventory if none exists.
        if (items != null)
        {
            playerInventoryDisplay.OnChangeInventory(items);
        }
        else
        {
            Debug.LogWarning("Inventory is empty.");
            items = new Dictionary<PickupItem.PickupType, int>();
        }
    }

    public void Add(PickupItem item)
    {
        // Determine the type of the collected item.
        PickupItem.PickupType type = item.Type;

        // Add or update the item count in the inventory.
        if (items.TryGetValue(type, out int oldTotal))
        {
            items[type] = oldTotal + 1;
        }
        else
        {
            items.Add(type, 1);
        }

        // Update the UI and save the updated inventory.
        playerInventoryDisplay.OnChangeInventory(items);
        DataManager.SaveInventory(items);
    }
}
