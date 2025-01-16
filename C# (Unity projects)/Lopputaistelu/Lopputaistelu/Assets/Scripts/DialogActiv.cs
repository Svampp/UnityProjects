using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles the activation of dialog sequences when the player interacts with NPCs or objects.
/// </summary>
public class DialogActivator : MonoBehaviour
{
    [SerializeField] private string[] lines; // Array of dialog lines for the conversation.

    [field: SerializeField] public bool IsPerson { get; private set; } // Indicates if the dialog is with a person.
    public bool CanDialogActivated { get; private set; } // Tracks if dialog can be activated.
    public bool IsDialogStarted { get; private set; } // Tracks if dialog has started.

    private Mouse myMouse; // Reference to the current mouse input.

    public bool isDialogueFinished = false; // Tracks if the dialog sequence has been completed.

    /// <summary>
    /// Initializes the mouse input reference.
    /// </summary>
    private void Start()
    {
        myMouse = Mouse.current;
    }

    /// <summary>
    /// Checks for dialog activation or completion during gameplay.
    /// </summary>
    private void Update()
    {
        // Start dialog when conditions are met.
        if (CanDialogActivated && myMouse.leftButton.wasPressedThisFrame && !IsDialogStarted && !isDialogueFinished)
        {
            StartDialog();
        }

        // Check if dialog has finished and take appropriate action.
        if (IsDialogStarted && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            OnDialogueFinished();
        }
    }

    /// <summary>
    /// Starts the dialog sequence.
    /// </summary>
    public void StartDialog()
    {
        IsDialogStarted = true;
        DialogManager.instance.ShowDialog(lines, IsPerson); // Show dialog using the DialogManager.
    }

    /// <summary>
    /// Handles the end of the dialog sequence.
    /// </summary>
    public void OnDialogueFinished()
    {
        IsDialogStarted = false;
        isDialogueFinished = true;

        // Notify any associated EnemyController that dialog has finished.
        EnemyController enemyController = GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.OnDialogueFinished();
        }
    }

    /// <summary>
    /// Detects when the player enters the dialog trigger area.
    /// </summary>
    /// <param name="collision">The collision object that entered the trigger.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDialogueFinished)
        {
            CanDialogActivated = true; // Allow dialog activation.
        }
    }

    /// <summary>
    /// Detects when the player exits the dialog trigger area.
    /// </summary>
    /// <param name="collision">The collision object that exited the trigger.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanDialogActivated = false; // Disable dialog activation.
        }
    }
}
