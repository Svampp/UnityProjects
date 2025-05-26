using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script handles dialog activation when the player interacts with NPCs or objects,
/// including starting and finishing dialogs and triggering related actions.
/// </summary>
public class DialogActivator : MonoBehaviour
{
    // Lines of dialog associated with this activator
    [SerializeField] private string[] lines;

    // Indicates if the activator represents a person (NPC)
    [field: SerializeField] public bool IsPerson { get; private set; }

    // Whether the dialog can be activated
    public bool CanDialogActivated { get; private set; }

    // Indicates if a dialog is currently active
    public bool IsDialogStarted { get; private set; }

    private Mouse myMouse;

    // Object to activate after the dialog is finished
    public GameObject objectToActivate;
    private bool isDialogueFinished = false;

    // Quest-related properties
    public bool isQuest;
    private QuestManager questManager;
    public int questNumber;

    /// <summary>
    /// Initializes the dialog activator and retrieves required components.
    /// </summary>
    private void Start()
    {
        myMouse = Mouse.current;
        questManager = FindObjectOfType<QuestManager>();
    }

    /// <summary>
    /// Handles dialog activation input and checks for dialog completion.
    /// </summary>
    private void Update()
    {
        // Start dialog when the left mouse button is pressed
        if (CanDialogActivated && myMouse.leftButton.wasPressedThisFrame && !IsDialogStarted)
        {
            StartDialog();
        }

        // Check if the dialog is finished and perform actions
        if (IsDialogStarted && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            OnDialogueFinished();
        }
    }

    /// <summary>
    /// Starts the dialog and displays it using the DialogManager.
    /// </summary>
    private void StartDialog()
    {
        IsDialogStarted = true;
        DialogManager.instance.ShowDialog(lines, IsPerson);
    }

    /// <summary>
    /// Handles actions when the dialog is finished.
    /// </summary>
    public void OnDialogueFinished()
    {
        IsDialogStarted = false;
        isDialogueFinished = true;
        ActivateObject();

        if (isQuest)
        {
            StartQuest();
        }
    }

    /// <summary>
    /// Activates an associated object, if any.
    /// </summary>
    private void ActivateObject()
    {
        if (isDialogueFinished && objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if the player enters the activation zone and allows dialog activation.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanDialogActivated = true;

            if (isQuest && isDialogueFinished)
            {
                StartQuest();
            }
        }
    }

    /// <summary>
    /// Starts a quest associated with this dialog activator.
    /// </summary>
    void StartQuest()
    {
        questManager.quests[questNumber].gameObject.SetActive(true);
        questManager.quests[questNumber].StartQuest();

        gameObject.SetActive(false);
    }
}
