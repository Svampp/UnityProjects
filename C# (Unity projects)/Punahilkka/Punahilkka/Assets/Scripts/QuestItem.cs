using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an item required for completing a quest.
/// Handles interactions with the player and quest logic.
/// </summary>
public class QuestItem : MonoBehaviour
{
    public int questNumber; // Associated quest number
    private QuestManager questManager; // Reference to the quest manager
    public string itemName; // Name of the item

    /// <summary>
    /// Retrieves the QuestManager at the start.
    /// </summary>
    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    /// <summary>
    /// Checks for player interaction and updates quest progress.
    /// </summary>
    /// <param name="collision">The collider of the object interacting with the item.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!questManager.questCompleted[questNumber] &&
                questManager.quests[questNumber].gameObject.activeSelf)
            {
                questManager.itemCollected = itemName; // Mark the item as collected
                AudioManager.instance.Play("QuestItem"); // Play sound effect
                gameObject.SetActive(false); // Disable the item
            }
        }
    }
}
