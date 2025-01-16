/// <summary>
/// Updates the UI to display interaction prompts to the player.
/// </summary>
public class PlayerInteractUI : MonoBehaviour
{
    // UI container for the interaction prompt.
    [SerializeField] GameObject container;

    // Text field for displaying the interaction text.
    [SerializeField] TMP_Text actionText;

    void Update()
    {
        // Show the interaction prompt if an interactable object is nearby; otherwise, hide it.
        if (PlayerInteract.Instance.GetInteract() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        // Display the UI container and update the interaction text.
        container.SetActive(true);
        actionText.text = PlayerInteract.Instance.GetInteract().GetInteractText();
    }

    void Hide()
    {
        // Hide the UI container.
        container?.SetActive(false);
    }
}
