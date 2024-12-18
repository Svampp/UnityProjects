using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyController : MonoBehaviour
{
    // Enemy's current state (idle or walking)
    [SerializeField]
    private EnemyState currentState;

    // Movement speed of the enemy
    public float moveSpeed;

    // Rigidbody2D for physics-based movement
    private Rigidbody2D myRigidbody;

    // Target to chase (typically the player)
    public Transform target;

    // Radius within which the enemy starts chasing the target
    public float chaseRadius;

    // Radius within which the enemy can attack the target
    public float attackRadius;

    // Animator for controlling enemy animations
    public Animator anim;

    // Damage dealt to the player when attacked
    [SerializeField]
    private int damageToGive;

    // Prefab for enemy's projectile attack
    [SerializeField]
    private GameObject projectile2Prefabs;

    // Variables for controlling the enemy's attack rate
    float nextFire;
    float fireRate = 4f;

    // Possible states for the enemy
    public enum EnemyState
    {
        idle,
        walk
    }

    private void Start()
    {
        // Initialize the enemy's state and components
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // Locate the player by tag
    }

    private void Update()
    {
        // Continuously check the distance to the target
        CheckDistance();
    }

    private void FixedUpdate()
    {
        // Draw a debug ray showing the direction toward the target
        Vector3 forward = transform.TransformDirection(target.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
    }

    private void CheckDistance()
    {
        // Calculate the distance between the enemy and the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If the target is within the chase radius
        if (distance <= chaseRadius)
        {
            // If the enemy is idle or walking, move toward the target
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                // Update the animation based on movement direction
                changeAnim(temp - transform.position);

                // Move the enemy toward the target
                myRigidbody.MovePosition(temp);

                // Transition to walking state if idle
                if (currentState == EnemyState.idle)
                {
                    ChangeState(EnemyState.walk);
                }

                anim.SetBool("Walking", true);
            }
        }

        // If the target is within the attack radius
        if (distance < attackRadius)
        {
            // Attack if the fire rate cooldown has passed
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot(); // Fire a projectile
                PlayerHealthManager.instance.HurtPlayer(damageToGive); // Inflict damage on the player
            }
        }
        // If the target is outside the chase radius
        else if (distance > chaseRadius)
        {
            anim.SetBool("Walking", false);
            ChangeState(EnemyState.idle); // Transition back to idle state
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        // Update the animator's movement direction parameters
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        // Determine the dominant movement direction and update the animator
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        // Change the enemy's state if it is different from the current state
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the chase radius in yellow
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        // Visualize the attack radius in blue
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Shoot()
    {
        // Instantiate a projectile prefab at the enemy's position
        GameObject projectile = Instantiate(projectile2Prefabs, transform.position, transform.rotation);
    }
}
