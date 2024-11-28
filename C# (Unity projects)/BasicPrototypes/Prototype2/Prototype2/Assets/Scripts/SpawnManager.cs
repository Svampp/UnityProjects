using UnityEngine;

// This script randomly spawns animals at a specified interval and position range.
public class SpawnManager : MonoBehaviour
{
    // Array to hold different animal prefabs to spawn.
    public GameObject[] animalPrefabs;
    // Horizontal range (x-axis) where animals can spawn.
    private float spawnRangeX = 20;
    // Fixed z-axis position where animals will spawn.
    private float spawnPosZ = 20;
    // Initial delay before spawning starts.
    private float startDelay = 2;
    // Time interval between spawns.
    private float spawnInterval = 1.5f;

    void Start()
    {
        // Repeatedly call the SpawnRandomAnimal method starting after `startDelay`
        // and continuing every `spawnInterval` seconds.
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Spawns a random animal at a random position within the spawn range.
    void SpawnRandomAnimal()
    {
        // Choose a random index from the animalPrefabs array.
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        // Calculate a random spawn position within the horizontal range and at the fixed z position.
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        // Instantiate the selected animal prefab at the calculated position with its original rotation.
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
