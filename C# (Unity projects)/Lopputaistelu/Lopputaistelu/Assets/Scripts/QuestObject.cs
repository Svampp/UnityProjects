using System.Collections;
using UnityEngine;

/// <summary>
/// Represents an individual quest, including its requirements, rewards, and progress.
/// </summary>
public class QuestObject : MonoBehaviour
{
    public int questNumber; // Unique identifier for the quest.
    public string[] lines; // Dialog lines associated with the quest.
    public QuestManager questManager; // Reference to the QuestManager.
    public bool isItemQuest; // Indicates if the quest involves collecting items.
    public string targetItem; // The specific item required for the quest.
    public int itemCollect; // The number of items required to complete the quest.
    public int itemCollectCount; // Tracks the number of collected items.

    [SerializeField]
    private int EXPAmmount; // Amount of experience rewarded upon quest completion.
    public int HPAmmount; // Amount of health rewarded upon quest completion.

    private bool isQuestCompleted = false; // Tracks if the quest is completed.

    /// <summary>
    /// Updates the progress of item collection quests.
    /// </summary>
    void Update()
    {
        if (isItemQuest && !isQuestCompleted)
        {
            if (questManager.itemCollected == targetItem)
            {
                questManager.itemCollected = null; // Clear the collected item.
                itemCollectCount++; // Increment the collected item count.
            }

            if (itemCollectCount >= itemCollect)
            {
                EndQuest(); // Complete the quest if the required items are collected.
            }
        }
    }

    /// <summary>
    /// Starts the quest by showing the initial quest dialog.
    /// </summary>
    public void StartQuest()
    {
        if (!isQuestCompleted)
        {
            questManager.ShowQuestText(lines[0]); // Display the starting quest text.
        }
    }

    /// <summary>
    /// Completes the quest, rewards the player, and deactivates the quest object.
    /// </summary>
    public void EndQuest()
    {
        questManager.ShowQuestText(lines[1]); // Display the completion text.
        questManager.questCompleted[questNumber] = true; // Mark the quest as completed.
        PlayerHealthManager.instance.AddPlayerEXP(EXPAmmount); // Reward experience points.
        PlayerHealthManager.instance.AddPlayerHealth(HPAmmount); // Reward health points.
        gameObject.SetActive(false); // Deactivate the quest object.

        isQuestCompleted = true; // Mark the quest as completed.
    }
}
