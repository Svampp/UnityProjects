/// <summary>
/// Represents a door that can be interacted with, triggering an animation and disabling further interactions.
/// </summary>
public class DoorInteractable : MonoBehaviour, IInteractable
{
    // Reference to the Animator component for controlling animations.
    Animator anim;

    void Awake()
    {
        // Initialize the Animator component.
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Provides the text displayed to the player when interacting with the door.
    /// </summary>
    public string GetInteractText()
    {
        return "Avaa ovi!"; // "Open door!" in Finnish.
    }

    /// <summary>
    /// Triggers the door opening animation and disables further interactions.
    /// </summary>
    public void Interact()
    {
        anim.SetTrigger("OpenDoor"); // Activate the "OpenDoor" animation.
        StopInteract(); // Disable interaction.
    }

    /// <summary>
    /// Disables the door's collider to prevent further interaction.
    /// </summary>
    public void StopInteract()
    {
        GetComponent<Collider>().enabled = false;
    }
}
