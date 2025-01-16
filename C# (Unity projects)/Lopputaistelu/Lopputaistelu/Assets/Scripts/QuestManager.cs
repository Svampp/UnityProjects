using System.Collections;
using UnityEngine;

/// <summary>
/// Manages quests in the game, including their completion status and task updates.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public QuestObject[] quests; // Array of all quests in the game.
    public bool[] questCompleted; // Tracks whether each quest is completed.
    public string itemCollected; // Tracks the most recently collected item.

    /// <summary>
    /// Initializes the quest completion array based on the number of quests.
    /// </summary>
    private void Start()
    {
        if (quests != null)
        {
            questCompleted = new bool[quests.Length];
        }
    }

    /// <summary>
    /// Displays the text of a specific quest task using the DialogManager.
    /// </summary>
    /// <param name="questTask">The task description to display.</param>
    public void ShowQuestText(string questTask)
    {
        string[] oneLine = new string[1];
        oneLine[0] = questTask;

        DialogManager.instance.ShowDialog(oneLine, false); // Display the quest task in a dialog box.
    }
}
