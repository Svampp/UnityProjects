using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Array to store all quest objects in the game
    public QuestObject[] quests;

    // Tracks whether each quest in the array has been completed
    public bool[] questCompleted;

    // Stores the name of the item currently collected by the player
    public string itemCollected;

    private void Start()
    {
        // Initialize the questCompleted array with the same length as the quests array
        if (quests != null)
        {
            questCompleted = new bool[quests.Length];
        }
    }

    // Displays a quest-related text to the player
    public void ShowQuestText(string questTask)
    {
        // Create an array with one element to pass to the dialog manager
        string[] oneLine = new string[1];
        oneLine[0] = questTask;

        // Use the DialogManager to show the quest text
        DialogManager.instance.ShowDialog(oneLine, false);
    }
}
