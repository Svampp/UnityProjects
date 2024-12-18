using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogActivator : MonoBehaviour
{
    // Array of dialog lines to display
    [SerializeField] private string[] lines;

    // Determines if the speaker is a person (for name display)
    [field: SerializeField] public bool IsPerson { get; private set; }

    // Flags for dialog activation and progress
    public bool CanDialogActivated { get; private set; }
    public bool IsDialogStarted { get; private set; }

    // Input reference for detecting interactions
    private Mouse myMouse;

    // Objects to activate after dialog
    public GameObject objectToActivate;
    public GameObject objectToActivate2;
    public GameObject objectToActivate3;

    // Tracks if the dialog sequence has finished
    private bool isDialogueFinished = false;

    // Quest-related variables
    public bool isQuest;
    private QuestManager questManager;
    public int questNumber;

    private void Start()
    {
        // Initialize mouse input and find the QuestManager
        myMouse = Mouse.current;
        questManager = FindObjectOfType<QuestManager>();

        // Deactivate the quest initially
        questManager.quests[questNumber].gameObject.SetActive(false);
    }

    private void Update()
    {
        // Start dialog if conditions are met
        if (CanDialogActivated && myMouse.leftButton.wasPressedThisFrame && !IsDialogStarted)
        {
            StartDialog();
        }

        // Check if the dialog has finished and trigger post-dialogue actions
        if (IsDialogStarted && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            OnDialogueFinished();
        }
    }

    private void StartDialog()
    {
        // Mark dialog as started and show the dialog lines
        IsDialogStarted = true;
        DialogManager.instance.ShowDialog(lines, IsPerson);
    }

    public void OnDialogueFinished()
    {
        // Mark dialog as finished and activate related objects
        IsDialogStarted = false;
        isDialogueFinished = true;
        ActivateObject();

        // Start a quest if applicable
        if (isQuest)
        {
            StartQuest();
        }
    }

    private void ActivateObject()
    {
        // Activate objects only if they are assigned and the dialog is finished
        if (isDialogueFinished && objectToActivate != null && objectToActivate2 != null && objectToActivate3 != null)
        {
            objectToActivate.SetActive(true);
            objectToActivate2.SetActive(true);
            objectToActivate3.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Allow dialog activation when the player enters the trigger zone
        if (collision.CompareTag("Player"))
        {
            CanDialogActivated = true;

            // If the dialog is finished and it's a quest, start the quest
            if (isQuest && isDialogueFinished)
            {
                StartQuest();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Prevent dialog activation when the player leaves the trigger zone
        if (collision.CompareTag("Player"))
        {
            CanDialogActivated = false;
        }
    }

    private void StartQuest()
    {
        // Activate and start the quest using the QuestManager
        questManager.quests[questNumber].gameObject.SetActive(true);
        questManager.quests[questNumber].StartQuest();
    }
}
