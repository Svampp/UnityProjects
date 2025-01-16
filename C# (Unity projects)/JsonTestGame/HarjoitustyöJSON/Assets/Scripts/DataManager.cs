using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// A static class to manage saving and loading inventory data.
public static class DataManager
{
    // Saves the inventory to a JSON file.
    public static void SaveInventory(Dictionary<PickupItem.PickupType, int> inventory)
    {
        // Serialize the inventory dictionary to a JSON string.
        string json = JsonConvert.SerializeObject(inventory);

        // Define the file path where the JSON file will be saved.
        string filePath = $"{Application.dataPath}/playerInventory.json";

        // Write the JSON string to the file.
        File.WriteAllText(filePath, json);
    }

    // Loads the inventory from a JSON file.
    public static Dictionary<PickupItem.PickupType, int> LoadInventory()
    {
        // Define the file path where the JSON file is located.
        string filePath = $"{Application.dataPath}/playerInventory.json";

        // Check if the file exists.
        if (File.Exists(filePath))
        {
            // Read the JSON string from the file.
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON string back into a dictionary and return it.
            return JsonConvert.DeserializeObject<Dictionary<PickupItem.PickupType, int>>(json);
        }
        else
        {
            // Log a warning message if the file doesn't exist.
            Debug.LogWarning("Inventory file not found!");

            // Return an empty inventory dictionary.
            return new Dictionary<PickupItem.PickupType, int>();
        }
    }
}
