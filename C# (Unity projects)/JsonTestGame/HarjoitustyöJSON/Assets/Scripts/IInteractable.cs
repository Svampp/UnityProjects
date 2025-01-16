/// <summary>
/// Interface for objects that can be interacted with.
/// </summary>
public interface IInteractable
{
    // Trigger an interaction with the object.
    void Interact();

    // Stop the interaction with the object.
    void StopInteract();
}
