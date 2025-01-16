/// <summary>
/// Handles player interaction with interactable objects in the scene.
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    // Singleton instance of the PlayerInteract class.
    public static PlayerInteract Instance { get; private set; }

    // Reference to the keyboard input system.
    Keyboard keyboard;

    void Start()
    {
        // Initialize the singleton instance and keyboard reference.
        Instance = this;
        keyboard = Keyboard.current;
    }

    void Update()
    {
        // Continuously check for interaction input.
        Interact();
    }

    void Interact()
    {
        // Check if the "E" key is pressed.
        if (keyboard.eKey.IsPressed())
        {
            // Get the nearest interactable object.
            IInteractable interactable = GetInteract();

            // Trigger the interaction if an interactable object is found.
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    /// <summary>
    /// Finds the nearest interactable object within range.
    /// </summary>
    /// <returns>An IInteractable object if found; otherwise, null.</returns>
    public IInteractable GetInteract()
    {
        float interactRange = 1f;

        // Check for colliders within the interaction range.
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            // Return the first interactable object found.
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }

        return null; // Return null if no interactable object is found.
    }
}
