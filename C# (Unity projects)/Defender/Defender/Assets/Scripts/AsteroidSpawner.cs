using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Objecti joka luodaan")]
    // The prefab of the asteroid that will be spawned
    [SerializeField] private GameObject prefabToSpawn;

    [Header("Ilmestymisnopeus")]
    // The interval at which asteroids will be spawned (in seconds)
    [SerializeField] private float spawnInterval = 1f;

    // BoxCollider2D to define the spawn area
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        // Get the BoxCollider2D component attached to the GameObject to define the spawn area
        boxCollider2D = GetComponent<BoxCollider2D>();

        // Start the coroutine that spawns objects
        StartCoroutine(SpawnObject());
    }

    // Coroutine that spawns asteroids at random positions within the BoxCollider2D bounds
    IEnumerator SpawnObject()
    {
        // Infinite loop to continuously spawn asteroids at the set interval
        while (true)
        {
            // Generate random X and Y positions within the bounds of the BoxCollider2D
            // The positions are offset by half the size of the collider's bounds to center the spawn area around the collider's position
            float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * 0.5f;
            float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * 0.5f;

            // Instantiate a new asteroid object from the prefab
            GameObject newAsteroid = Instantiate(prefabToSpawn);

            // Set the position of the new asteroid to a random location within the collider's bounds
            newAsteroid.transform.position = new Vector2(randomX + transform.position.x, randomY + transform.position.y);

            // Wait for the specified interval before spawning the next asteroid
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
