using UnityEngine;

/// <summary>
/// Represents an item that can be picked up by the player.
/// </summary>
public class PickupItem : MonoBehaviour
{
    // Enumeration of the types of items that can be picked up.
    public enum PickupType { Barrel, Crate, Barrier, Cone, Spool, RockBoulder }

    // The type of the pickup item.
    [field: SerializeField] public PickupType Type { get; set; }
}
