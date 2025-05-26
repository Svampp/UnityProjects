/// <summary>
/// Represents a chest that can be interacted with, triggering an animation and disabling further interactions.
/// </summary>
public class ChestInteractable : MonoBehaviour, IInteractable
{
    // Reference to the Animator component for controlling animations.
    Animator anim;

    /// <summary>
    /// Provides the text displayed to the player when interacting with the chest.
    /// </summary>
    /// <returns>A string prompting the player to open the chest.</returns>
    public string GetInteractText()
    {
        return "Avaa arkku"; // "Open chest" in Finnish.
    }

    /// <summary>
    /// Triggers the chest opening animation and disables further interactions.
    /// </summary>
    public void Interact()
    {
        // Trigger the "OpenChest" animation.
        anim.SetTrigger("OpenChest");

        // Stop further interaction by disabling the collider.
        StopInteract();
    }

    /// <summary>
    /// Disables the chest's collider to prevent further interaction.
    /// </summary>
    public void StopInteract()
    {
        GetComponent<Collider>().enabled = false;
    }

    void Awake()
    {
        // Get the Animator component attached to the chest.
        anim = GetComponent<Animator>();
    }
}
