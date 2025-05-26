using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all quests in the game, including tracking progress,
/// completed quests, and showing quest-related dialogs.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests; // Array of all quests in the game
    public bool[] questCompleted; // Tracks whether each quest is completed
    public string itemCollected; // Name of the most recently collected item

    /// <summary>
    /// Initializes the quest completion array.
    /// </summary>
    private void Start()
    {
        questCompleted = new bool[questCompleted.Length];
    }

    /// <summary>
    /// Displays quest-related text using the DialogManager.
    /// </summary>
    /// <param name="questTask">The text to display for the quest.</param>
    public void ShowQuestText(string questTask)
    {
        string[] oneLine = new string[1];
        oneLine[0] = questTask;

        DialogManager.instance.ShowDialog(oneLine, false); // Show the text as a dialog
    }
}
