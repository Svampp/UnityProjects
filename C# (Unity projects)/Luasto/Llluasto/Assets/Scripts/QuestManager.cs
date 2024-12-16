using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Array of QuestObject references, representing the quests in the game
    public QuestObject[] quests;

    // Boolean array that tracks whether each quest has been completed
    public bool[] questCompleted;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the quests array is not null
        if (quests != null)
        {
            // Initialize the questCompleted array based on the number of quests
            questCompleted = new bool[quests.Length];

            // Start all quests at the beginning of the game
            StartAllQuests();
        }
    }

    // Starts all quests by calling the StartQuest method on each QuestObject
    void StartAllQuests()
    {
        // Loop through each quest in the quests array
        foreach (var quest in quests)
        {
            // Call the StartQuest method on the current quest to initialize it
            quest.StartQuest();
        }
    }
}
