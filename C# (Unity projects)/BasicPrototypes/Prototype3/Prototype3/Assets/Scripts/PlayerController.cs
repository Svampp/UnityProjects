using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script controls the player's movement, animations, and game-over logic.
public class PlayerController : MonoBehaviour
{
    // Components and systems.
    private Rigidbody playerRb; // Rigidbody for physics-based movement.
    private Animator playerAnim; // Animator for triggering animations.
    private AudioSource playerAudio; // AudioSource for sound effects.

    // Effects and sounds.
    public ParticleSystem explosionParticle; // Particle effect for when the player crashes.
    public ParticleSystem dirtParticle; // Particle effect for dust while running.
    public AudioClip jumpSound; // Sound effect for jumping.
    public AudioClip crashSound; // Sound effect for crashing.

    // Player mechanics.
    public float jumpForce = 8; // Upward force applied when the player jumps.
    public float gravityModifier; // Gravity multiplier to adjust the game's gravity.
    public bool isOnGround = true; // Indicates if the player is currently on the ground.
    public bool gameOver; // Indicates if the game is over.

    // Score tracking.
    private int jumpCounter = 0; // Counts the number of jumps, used to track score.

    void Start()
    {
        // Initialize references to components.
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        // Modify gravity to make jumps feel more realistic.
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        // Check for jump input and ensure the player is on the ground and the game is not over.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Apply an upward force to the player for the jump.
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Update state variables and animations.
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");

            // Stop the dirt particle effect while in the air.
            dirtParticle.Stop();

            // Play the jump sound.
            playerAudio.PlayOneShot(jumpSound, 2.0f);

            // Increment the jump counter and update the high score.
            jumpCounter++;
            ScoreManager.instance.UpdateHighScore(jumpCounter);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player landed on the ground.
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play(); // Resume the dirt particle effect.
        }
        // Check if the player collided with an obstacle.
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true; // Mark the game as over.

            // Trigger death animations and effects.
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play(); // Play the explosion effect.
            dirtParticle.Stop(); // Stop the dirt effect.
            playerAudio.PlayOneShot(crashSound, 1.0f); // Play the crash sound.

            // Restart the game after a delay.
            StartCoroutine(RestartGameWithDelay(3f));
        }
    }

    // Coroutine to restart the game after a delay.
    private IEnumerator RestartGameWithDelay(float delay)
    {
        // Wait for the specified delay time.
        yield return new WaitForSeconds(delay);

        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
