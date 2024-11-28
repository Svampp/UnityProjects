using UnityEngine;

// This script spawns obstacles at regular intervals as long as the game is not over.
public class SpawnManager : MonoBehaviour
{
    // Prefab of the obstacle to be spawned.
    public GameObject obstaclePrefab;
    // Position where obstacles will spawn.
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    // Initial delay before spawning starts.
    private float startDelay = 2;
    // Interval at which obstacles are spawned.
    private float repeatRate = 2;
    // Reference to the PlayerController script to check the game state.
    private PlayerController playerControllerScript;

    void Start()
    {
        // Find the Player GameObject and get the PlayerController script
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Schedule the SpawnObstacle method to be repeatedly called after a delay.
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Spawns an obstacle if the game is not over.
    void SpawnObstacle()
    {
        // Check if the game is still active.
        if (playerControllerScript.gameOver == false)
        {
            // Instantiate the obstacle prefab at the defined position with its default rotation.
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }

    }
}
