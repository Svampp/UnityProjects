using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a quest in the game, tracking objectives and completion state.
/// </summary>
public class QuestObject : MonoBehaviour
{
    public int questNumber; // Unique identifier for the quest
    public string[] lines; // Dialog lines related to the quest
    public QuestManager questManager; // Reference to the QuestManager

    public bool isItemQuest; // Indicates if the quest involves collecting items
    public string targetItem; // The name of the item to collect
    public int itemCollect; // Number of items required to complete the quest
    public int itemCollectCount; // Current number of collected items

    [SerializeField]
    private int EXPammount; // Experience rewarded upon quest completion

    /// <summary>
    /// Updates the quest state, checking for item collection progress.
    /// </summary>
    private void Update()
    {
        if (isItemQuest)
        {
            if (questManager.itemCollected == targetItem)
            {
                questManager.itemCollected = null;
                itemCollectCount++;
            }

            if (itemCollectCount >= itemCollect)
            {
                EndQuest(); // Complete the quest
            }
        }
    }

    /// <summary>
    /// Starts the quest, optionally displaying an initial dialog.
    /// </summary>
    public void StartQuest()
    {
        if (questNumber != 1)
        {
            questManager.ShowQuestText(lines[0]); // Show starting dialog
        }
    }

    /// <summary>
    /// Ends the quest, awards experience, and triggers related actions.
    /// </summary>
    public void EndQuest()
    {
        if (questNumber == 1)
        {
            questManager.questCompleted[questNumber] = true;
            AudioManager.instance.StopPlay("Background");
            AudioManager.instance.Play("Piano");
            AudioManager.instance.Play("Background");
        }
        else
        {
            questManager.ShowQuestText(lines[1]); // Show completion dialog
            questManager.questCompleted[questNumber] = true;
            PlayerHealthManager.instance.AddPlayerEXP(EXPammount); // Reward experience
            gameObject.SetActive(false); // Disable the quest object
        }
    }
}
