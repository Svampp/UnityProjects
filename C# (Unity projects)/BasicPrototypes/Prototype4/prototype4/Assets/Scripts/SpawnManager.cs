using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Reference to the enemy prefab to spawn
    public GameObject enemyPrefab;

    // Reference to the powerup prefab to spawn
    public GameObject powerupPrefab;

    // Defines the range within which enemies and powerups can spawn
    private float spawnRange = 9;

    // Tracks the number of enemies currently in the scene
    public int enemyCount;

    // Tracks the current wave number
    public int waveNumber = 1;

    void Start()
    {
        // Spawns the first wave of enemies
        SpawnEnemyWave(waveNumber);

        // Spawns the first powerup
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Generates a random spawn position within the defined range
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange); // Random X coordinate
        float spawnPosZ = Random.Range(-spawnRange, spawnRange); // Random Z coordinate
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ); // Combine into a Vector3
        return randomPos;
    }

    void Update()
    {
        // Updates the current enemy count by checking all objects of type "Enemy"
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // If no enemies are left, prepare the next wave
        if (enemyCount == 0)
        {
            waveNumber++; // Increment wave number
            SpawnEnemyWave(waveNumber); // Spawn a new wave of enemies

            // Spawn a new powerup
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    // Spawns a wave of enemies based on the number specified
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++) // Loop through the number of enemies to spawn
        {
            // Instantiate an enemy at a random spawn position
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
