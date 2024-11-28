using UnityEngine;

// This script moves a GameObject to the left and destroys obstacles that go out of bounds.
public class MoveLeft : MonoBehaviour
{
    // Speed at which the GameObject moves to the left.
    private float speed = 30;
    // Reference to the PlayerController script to check if the game is over.
    private PlayerController playerControllerScript;
    // X-axis boundary at which obstacles are destroyed.
    private float leftBound = -15;

    void Start()
    {
        // Find the Player GameObject and get the PlayerController script to check the game state.
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Move the GameObject to the left if the game is not over.
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // Destroy the GameObject if it is tagged as "Obstacle" and moves out of bounds.
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }
}
