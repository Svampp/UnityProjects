using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages the display and flow of dialog sequences, including handling text and character names.
/// </summary>
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance; // Singleton instance for global access.

    private string[] dialogLines; // Array of dialog lines for the current conversation.
    private int currentLine; // Index of the current dialog line.
    private bool justStarted; // Indicates if the dialog has just started.

    [Header("CANVAS")]
    [SerializeField] private TMP_Text dialogText; // UI text element for dialog content.
    [SerializeField] private TMP_Text nameText; // UI text element for character names.
    [SerializeField] public GameObject dialogBox; // Dialog box GameObject.
    [SerializeField] private GameObject nameBox; // Name box GameObject.

    private Mouse myMouse; // Reference to the current mouse input.

    /// <summary>
    /// Initializes the dialog manager instance and input references.
    /// </summary>
    private void Start()
    {
        instance = this;
        myMouse = Mouse.current;
    }

    /// <summary>
    /// Handles dialog progression based on input.
    /// </summary>
    private void Update()
    {
        // Progress dialog if the right mouse button is pressed.
        if (justStarted && myMouse.rightButton.wasPressedThisFrame)
        {
            currentLine++;
            if (currentLine >= dialogLines.Length)
            {
                StopDialog(); // End dialog when all lines are shown.
            }
            else
            {
                CheckIfName(); // Update name display if needed.
                dialogText.text = dialogLines[currentLine]; // Display the next line.
            }
        }
    }

    /// <summary>
    /// Starts a new dialog sequence.
    /// </summary>
    /// <param name="newLines">Array of dialog lines to display.</param>
    /// <param name="isPerson">Indicates if the dialog involves a person.</param>
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        dialogLines = newLines;
        currentLine = 0;
        CheckIfName(); // Check if the first line contains a name.

        nameBox.SetActive(isPerson); // Show or hide the name box.
        dialogText.text = dialogLines[currentLine]; // Display the first line.
        dialogBox.SetActive(true); // Show the dialog box.
        justStarted = true; // Mark the start of the dialog.
    }

    /// <summary>
    /// Stops the current dialog sequence and hides the dialog box.
    /// </summary>
    public void StopDialog()
    {
        dialogBox.SetActive(false);
    }

    /// <summary>
    /// Checks if the current line starts with a name identifier and updates the name box accordingly.
    /// </summary>
    private void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", ""); // Extract the name.
            currentLine++; // Skip the name line.
        }
    }
}
