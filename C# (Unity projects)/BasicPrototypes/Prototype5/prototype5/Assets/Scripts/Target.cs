using UnityEngine;

public class Target : MonoBehaviour
{
    // Reference to the Rigidbody for physics-based interactions
    private Rigidbody targetRb;

    // Reference to the GameManager script
    private GameManager gameManager;

    // Ranges for target movement and spawn position
    private float minSpeed = 12; // Minimum upward force applied to the target
    private float maxSpeed = 16; // Maximum upward force applied to the target
    private float maxTorque = 10; // Maximum rotational force applied to the target
    private float xRange = 4; // Horizontal range for spawning the target
    private float ySpawnPos = -2; // Vertical spawn position of the target

    // Particle effect for explosion when the target is destroyed
    public ParticleSystem explosionParticle;

    // Points awarded for destroying this target
    public int pointValue;

    void Start()
    {
        // Get the Rigidbody component attached to this target
        targetRb = GetComponent<Rigidbody>();

        // Find the GameManager object and get its GameManager component
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Apply a random upward force to launch the target
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        // Apply random torque for spinning the target
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // Set the initial spawn position to a random value within the specified range
        transform.position = RandomSpawnPos();
    }

    // Detects when the target is clicked
    private void OnMouseDown()
    {
        // Only respond to clicks if the game is active
        if (gameManager.isGameActive)
        {
            Destroy(gameObject); // Destroy the target object
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); // Play explosion particle effect
            gameManager.UpdateScore(pointValue); // Update the score in the GameManager
        }
    }

    // Detects when the target enters a trigger zone
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); // Destroy the target

        // If the target is not a "Bad" object, end the game
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver(); // Call the GameOver method in the GameManager
        }
    }

    // Generates a random upward force within the defined speed range
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Generates a random torque value for spinning the target
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Generates a random spawn position within the horizontal range and at a fixed vertical position
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
