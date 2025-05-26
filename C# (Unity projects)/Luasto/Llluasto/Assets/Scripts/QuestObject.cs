using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    // The quest number that identifies which quest this object is related to
    public int questNumber;

    // Array of enemies that need to be defeated for the quest to be completed
    public GameObject[] enemies;

    // Reference to the QuestManager to update quest completion status
    public QuestManager questManager;

    // Flag that keeps track of whether the quest is completed
    private bool isQuestCompleted = false;

    // List of GameObjects that should be activated when the quest is completed
    public List<GameObject> objectsToActivate = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // Check if the quest is not completed and if all enemies are defeated
        if (!isQuestCompleted && AreAllEnemiesDefeated())
        {
            // If all enemies are defeated, end the quest
            EndQuest();
        }
    }

    // Starts the quest (currently empty but can be expanded)
    public void StartQuest()
    {
        // If the quest is not already completed, this is where quest setup logic would go
        if (!isQuestCompleted)
        {
            
        }
    }

    // Checks if all enemies in the 'enemies' array have been defeated
    bool AreAllEnemiesDefeated()
    {
        // Loop through each enemy in the enemies array
        foreach (var enemy in enemies)
        {
            // If any enemy is not null (meaning it still exists in the scene), the quest isn't completed
            if (enemy != null)
            {
                return false;
            }
        }

        // If all enemies are null, the quest is considered completed
        return true;
    }

    // Ends the quest by marking it as complete and activating the necessary objects
    public void EndQuest()
    {
        // Mark the quest as completed in the QuestManager by setting the corresponding quest index to true
        questManager.questCompleted[questNumber] = true;

        // Activate all the objects associated with the quest (e.g., rewards, next level triggers)
        ActivateObjects();

        // Set the quest as completed so it won't trigger again
        isQuestCompleted = true;

        // Deactivate the QuestObject itself so it no longer affects the game
        gameObject.SetActive(false);
    }

    // Activates the objects that should be triggered when the quest is completed
    void ActivateObjects()
    {
        // Loop through all the objects in the objectsToActivate list
        foreach (var obj in objectsToActivate)
        {
            // If the object is not null, set it to be active
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
