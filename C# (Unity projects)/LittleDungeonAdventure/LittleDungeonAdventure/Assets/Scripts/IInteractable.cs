/// <summary>
/// Interface for objects that can be interacted with.
/// </summary>
public interface IInteractable
{
    void Interact(); // Trigger an interaction.
    void StopInteract(); // Stop the interaction.
    string GetInteractText(); // Provide a description of the interaction.
}
