using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    // Singleton instance for global access
    public static DialogManager instance;

    // Array of dialog lines to display
    private string[] dialogLines;

    // Index of the current line being displayed
    private int currentLine;

    // Flag to prevent unintended input during the initial frame of dialog display
    private bool justStarted;

    [Header("CANVAS")]
    // UI element for displaying dialog text
    [SerializeField] private TMP_Text dialogText;

    // UI element for displaying the speaker's name
    [SerializeField] private TMP_Text nameText;

    // UI GameObject containing the dialog box
    [SerializeField] public GameObject dialogBox;

    // UI GameObject containing the name box
    [SerializeField] private GameObject nameBox;

    // Reference to the mouse input
    private Mouse myMouse;

    private void Start()
    {
        // Initialize the singleton instance
        instance = this;

        // Reference to the current mouse device from the Input System
        myMouse = Mouse.current;
    }

    private void Update()
    {
        // Proceed to the next dialog line when the right mouse button is clicked
        if (justStarted && myMouse.rightButton.wasPressedThisFrame)
        {
            currentLine++;

            // If all lines have been displayed, stop the dialog
            if (currentLine >= dialogLines.Length)
            {
                StopDialog();
            }
            else
            {
                // Check for a speaker name tag and update the text
                CheckIfName();
                dialogText.text = dialogLines[currentLine];
            }
        }
    }

    // Starts a new dialog sequence
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        // Set the dialog lines and reset the line index
        dialogLines = newLines;
        currentLine = 0;

        // Check if the first line specifies a speaker name
        CheckIfName();

        // Show or hide the name box based on whether the speaker is a person
        nameBox.SetActive(isPerson);

        // Set the dialog text to the first line
        dialogText.text = dialogLines[currentLine];

        // Activate the dialog box
        dialogBox.SetActive(true);

        // Mark the start of the dialog
        justStarted = true;
    }

    // Stops the dialog and hides the dialog box
    public void StopDialog()
    {
        dialogBox.SetActive(false);
    }

    // Checks if the current line specifies a speaker's name and updates the name box
    void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            // Extract and set the name by removing the "n-" prefix
            nameText.text = dialogLines[currentLine].Replace("n-", "");

            // Skip to the next line since this line is the speaker's name
            currentLine++;
        }
    }
}
