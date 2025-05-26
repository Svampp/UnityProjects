using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyState currentState;  // Current state of the enemy (idle, walk)
    public float moveSpeed;  // Speed at which the enemy moves
    private Rigidbody2D myRigidbody;  // Rigidbody to move the enemy
    public Transform target;  // Target to chase (player)
    public float chaseRadius;  // Distance within which the enemy will chase the player
    public float attackRadius;  // Distance within which the enemy will attack the player
    public Animator anim;  // Animator to control enemy animations
    [SerializeField]
    private int damageToGive;  // Amount of damage the enemy deals to the player
    [SerializeField]
    private GameObject projectile2Prefabs;  // Projectile prefab the enemy will shoot

    float nextFire;  // Time for the next attack
    float fireRate = 4f;  // Delay between attacks (fire rate)

    // Enum for enemy states (idle, walk)
    public enum EnemyState
    {
        idle,
        walk
    }

    private void Start()
    {
        currentState = EnemyState.idle;  // Initially, the enemy is idle
        myRigidbody = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
        anim = GetComponent<Animator>();  // Get the Animator component
        target = GameObject.FindWithTag("Player").transform;  // Find the player
    }

    private void Update()
    {
        CheckDistance();  // Check the distance between the player and the enemy
    }

    private void FixedUpdate()
    {
        // Debug line to visualize the direction towards the player
        Vector3 forward = transform.TransformDirection(target.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
    }

    private void CheckDistance()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= chaseRadius)
        {
            // Move towards the player if within the chase radius
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);  // Update the animation based on movement direction
            myRigidbody.MovePosition(temp);  // Move the enemy towards the player

            if (currentState == EnemyState.idle)
            {
                ChangeState(EnemyState.walk);  // Change state to walk when moving
            }

            anim.SetBool("Walking", true);  // Set walking animation to true
        }

        // Attack the player if within attack radius
        if (distance < attackRadius)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();  // Shoot a projectile
                PlayerHealthManager.instance.HurtPlayer(damageToGive);  // Damage the player
            }
        }
        else if (distance > chaseRadius)
        {
            // Stop moving and set the state to idle if outside chase radius
            anim.SetBool("Walking", false);
            ChangeState(EnemyState.idle);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        // Set animation parameters for movement direction (MoveX, MoveY)
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        // Change animation based on movement direction (left, right, up, down)
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) SetAnimFloat(Vector2.right);  // Right
            else if (direction.x < 0) SetAnimFloat(Vector2.left);  // Left
        }
        else
        {
            if (direction.y > 0) SetAnimFloat(Vector2.up);  // Up
            else if (direction.y < 0) SetAnimFloat(Vector2.down);  // Down
        }
    }

    private void ChangeState(EnemyState newState)
    {
        // Change the enemy's current state if it's not the same as the new state
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the chase and attack radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void Shoot()
    {
        // Instantiate the projectile at the enemy's position and rotation
        GameObject projectile = Instantiate(projectile2Prefabs, transform.position, transform.rotation);
    }
}
