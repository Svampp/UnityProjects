using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Controls enemy behavior, including movement, animations, 
/// and interactions with the player based on distance.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyState currentState; // Current state of the enemy
    public float moveSpeed; // Movement speed of the enemy
    private Rigidbody2D myRigidbody; // Rigidbody component for physics interactions
    public Transform target; // Target to chase (player)
    public float chaseRadius; // Distance at which the enemy starts chasing
    public float attackRadius; // Distance at which the enemy starts attacking
    public Animator anim; // Animator for controlling animations
    [SerializeField]
    private int damageToGive; // Damage dealt to the player
    [SerializeField]
    private GameObject projectile2Prefabs; // Prefab for enemy projectiles

    float nextFire; // Time for the next attack
    float fireRate = 4f; // Attack cooldown time

    public enum EnemyState
    {
        idle, // Enemy is idle
        walk, // Enemy is walking
        run, // Enemy is running
        attack // Enemy is attacking
    }

    private void Start()
    {
        currentState = EnemyState.idle; // Initialize the enemy state
        myRigidbody = GetComponent<Rigidbody2D>(); // Get Rigidbody component
        anim = GetComponent<Animator>(); // Get Animator component
        target = GameObject.FindWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        // Logic in Update is intentionally left empty
    }

    private void FixedUpdate()
    {
        // Debugging ray showing the direction towards the player
        Vector3 forward = transform.TransformDirection(target.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        CheckDistance(); // Check the distance between enemy and player
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position); // Calculate distance to the player

        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.run)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime); // Move towards the player
                changeAnim(temp - transform.position); // Change animation based on movement
                myRigidbody.MovePosition(temp); // Update enemy position
                ChangeState(EnemyState.run); // Set state to running
                anim.SetBool("Running", true); // Enable running animation
            }
        }
        else if (distance > chaseRadius)
        {
            anim.SetBool("Running", false); // Disable running animation
            ChangeState(EnemyState.idle); // Set state to idle
        }
        else if (distance < attackRadius)
        {
            if (Time.time > nextFire) // Check if the enemy can attack
            {
                nextFire = Time.time + fireRate; // Update attack cooldown
                Shoot(); // Fire a projectile
                PlayerHealthManager.instance.HurtPlayer(damageToGive); // Deal damage to the player
            }
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x); // Update horizontal animation parameter
        anim.SetFloat("MoveY", setVector.y); // Update vertical animation parameter
    }

    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                print("Right"); // Debug message for right movement
                SetAnimFloat(Vector2.right); // Set right animation
            }
            else if (direction.x < 0)
            {
                print("Left"); // Debug message for left movement
                SetAnimFloat(Vector2.left); // Set left animation
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up); // Set up animation
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down); // Set down animation
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState; // Update state
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the chase radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        // Draw the attack radius in the editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void Shoot()
    {
        Instantiate(projectile2Prefabs, transform.position, transform.rotation); // Spawn a projectile
        AudioManager.instance.Play("ShootFromEnemy"); // Play shooting sound
    }
}
