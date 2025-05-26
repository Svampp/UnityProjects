using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
/// This script manages the dialog system, displaying dialog lines, handling
/// input for progressing through dialogs, and managing name displays for NPCs.
/// </summary>
public class DialogManager : MonoBehaviour
{
    // Singleton instance of DialogManager
    public static DialogManager instance;

    // Array of dialog lines to display
    private string[] dialogLines;

    // Index of the current dialog line
    private int currentLine;

    // Indicates if the dialog has just started
    private bool justStarted;

    [Header("CANVAS")]
    // Text field for displaying dialog
    [SerializeField] private TMP_Text dialogText;

    // Text field for displaying character names
    [SerializeField] private TMP_Text nameText;

    // UI element for the dialog box
    [SerializeField] public GameObject dialogBox;

    // UI element for the name box
    [SerializeField] private GameObject nameBox;

    private Mouse myMouse;

    /// <summary>
    /// Initializes the DialogManager and retrieves input devices.
    /// </summary>
    private void Start()
    {
        instance = this;
        myMouse = Mouse.current;
    }

    /// <summary>
    /// Checks for player input to progress through the dialog.
    /// </summary>
    private void Update()
    {
        // Progress dialog with the right mouse button
        if (justStarted && myMouse.rightButton.wasPressedThisFrame)
        {
            currentLine++;
            if (currentLine >= dialogLines.Length)
            {
                StopDialog();
            }
            else
            {
                CheckIfName();
                dialogText.text = dialogLines[currentLine];
            }
        }
    }

    /// <summary>
    /// Starts a dialog by displaying the dialog lines and activating the dialog box.
    /// </summary>
    /// <param name="newLines">Array of dialog lines to display.</param>
    /// <param name="isPerson">Whether the dialog is with a person (displays a name box).</param>
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        dialogLines = newLines;
        currentLine = 0;
        CheckIfName();
        nameBox.SetActive(isPerson);
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
        justStarted = true;
    }

    /// <summary>
    /// Stops the dialog and hides the dialog box.
    /// </summary>
    public void StopDialog()
    {
        dialogBox.SetActive(false);
    }

    /// <summary>
    /// Checks if the current dialog line contains a character name and adjusts the dialog accordingly.
    /// </summary>
    void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}
