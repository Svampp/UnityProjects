using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    // Unique identifier for the quest
    public int questNumber;

    // Dialogue lines for the quest (e.g., quest description or completion messages)
    public string[] lines;

    // Reference to the QuestManager to handle quest-related logic
    public QuestManager questManager;

    // Indicates if this is an item collection quest
    public bool isItemQuest;

    // Name of the item required to complete the quest
    public string targetItem;

    // Number of items required to complete the quest
    public int itemCollect;

    // Tracks how many items have been collected so far
    public int itemCollectCount;

    // Amount of experience points awarded upon quest completion
    [SerializeField]
    private int EXPAmmount;

    // Amount of health points restored upon quest completion
    public int HPAmmount;

    // Tracks whether the quest is completed
    private bool isQuestCompleted = false;

    void Update()
    {
        // Check if this is an item quest and the quest is not yet completed
        if (isItemQuest && !isQuestCompleted)
        {
            // Check if the required item has been collected
            if (questManager.itemCollected == targetItem)
            {
                // Reset the itemCollected field in the QuestManager
                questManager.itemCollected = null;

                // Increment the item collection count
                itemCollectCount++;
            }

            // If the required number of items has been collected, complete the quest
            if (itemCollectCount >= itemCollect)
            {
                EndQuest();
            }
        }
    }

    public void StartQuest()
    {
        // Start the quest if it has not been completed
        if (!isQuestCompleted)
        {
            // Display the starting dialogue for the quest
            questManager.ShowQuestText(lines[0]);
        }
    }

    public void EndQuest()
    {
        // Display the completion dialogue for the quest
        questManager.ShowQuestText(lines[1]);

        // Mark the quest as completed in the QuestManager
        questManager.questCompleted[questNumber] = true;

        // Award experience points and health points to the player
        PlayerHealthManager.instance.AddPlayerEXP(EXPAmmount);
        PlayerHealthManager.instance.AddPlayerHealth(HPAmmount);

        // Disable the quest object
        gameObject.SetActive(false);

        // Set the quest status to completed
        isQuestCompleted = true;
    }
}
