/// <summary>
/// Represents an item that can be picked up by the player, including keys and consumables.
/// </summary>
public class PickupItem : MonoBehaviour
{
    // Enumeration of the types of items that can be picked up.
    public enum PickupType { Key1, Key2, BluePotion }

    // Additional item properties.
    [SerializeField] int gold; // Gold value of the item.
    [SerializeField] int exp; // Experience value of the item.

    // The type of the pickup item.
    [field: SerializeField] public PickupType Type { get; set; }
}
