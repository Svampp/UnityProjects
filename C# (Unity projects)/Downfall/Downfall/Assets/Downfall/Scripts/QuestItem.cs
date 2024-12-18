using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    // Identifier for the quest associated with this item
    public int questNumber;

    // Reference to the QuestManager for managing quest logic
    private QuestManager questManager;

    // Name of the item, used to track collection in the QuestManager
    public string itemName;

    // Optional: GameObject to activate after the item is collected
    public GameObject objectToActivate;

    void Start()
    {
        // Find the QuestManager in the scene to use its functionalities
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Ensure the quest is not already completed and is currently active
            if (!questManager.questCompleted[questNumber] &&
                questManager.quests[questNumber].gameObject.activeSelf)
            {
                // Set the collected item's name in the QuestManager
                questManager.itemCollected = itemName;

                // Destroy this item after collection
                Destroy(gameObject);

                // Activate the linked GameObject, if assigned
                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);
                }
            }
        }
    }
}
