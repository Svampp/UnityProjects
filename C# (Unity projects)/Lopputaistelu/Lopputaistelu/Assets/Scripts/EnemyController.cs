using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls enemy behavior, including movement, attacking, and dialog interactions.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyState currentState; // Current state of the enemy (idle, chase, or attack).
    public float moveSpeed; // Speed of the enemy's movement.
    private Rigidbody2D myRigidbody; // Reference to the enemy's Rigidbody2D.
    public Transform target; // Target for the enemy to chase (usually the player).
    public float chaseRadius; // Radius within which the enemy will start chasing the target.
    public float attackRadius; // Radius within which the enemy will attack the target.
    public Animator anim; // Animator component for handling animations.

    [SerializeField]
    private int damageToGive; // Damage dealt to the target.
    [SerializeField]
    private GameObject projectile2Prefabs; // Prefab for enemy projectiles.
    public float fireRate = 3f; // Frequency of projectile firing.

    private TimerManager battleManager; // Reference to the battle timer manager.
    private DialogActivator dialogActivator; // Reference to dialog activator for interactions.
    private bool isDialogueFinished = false; // Tracks if the dialog sequence has finished.

    // Enum representing the states of the enemy.
    public enum EnemyState
    {
        idle,
        chase,
        attack
    }

    /// <summary>
    /// Initializes the enemy's state and references.
    /// </summary>
    private void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        InvokeRepeating("Shoot", fireRate, fireRate); // Schedule projectile shooting.

        battleManager = FindObjectOfType<TimerManager>();
        dialogActivator = GetComponent<DialogActivator>();
    }

    /// <summary>
    /// Updates the enemy's behavior based on the distance to the target.
    /// </summary>
    private void Update()
    {
        CheckDistance();
    }

    /// <summary>
    /// Draws debug gizmos to visualize attack and chase radii in the scene.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(target.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
    }

    /// <summary>
    /// Checks the distance to the target and adjusts the enemy's state accordingly.
    /// </summary>
    private void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (dialogActivator != null && !isDialogueFinished)
        {
            return; // Wait until the dialog sequence is completed.
        }

        if (distance <= chaseRadius && distance > attackRadius)
        {
            if (currentState != EnemyState.chase)
            {
                ChangeState(EnemyState.chase);
                anim.SetBool("Attack", false);
            }
            MoveTowardsPlayer();
        }
        else if (distance <= attackRadius)
        {
            if (currentState != EnemyState.attack)
            {
                ChangeState(EnemyState.attack);
                anim.SetBool("Attack", true);
            }
            MoveTowardsPlayer();
        }
        else if (distance > chaseRadius)
        {
            anim.SetBool("Attack", false);
            ChangeState(EnemyState.idle);
        }
    }

    /// <summary>
    /// Moves the enemy towards the player's position.
    /// </summary>
    private void MoveTowardsPlayer()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position); // Update animation based on movement direction.
        myRigidbody.MovePosition(temp);
    }

    /// <summary>
    /// Changes the enemy's animation based on the direction of movement.
    /// </summary>
    private void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetAnimFloat(direction.x > 0 ? Vector2.right : Vector2.left);
        }
        else
        {
            SetAnimFloat(direction.y > 0 ? Vector2.up : Vector2.down);
        }
    }

    /// <summary>
    /// Updates animation parameters for direction.
    /// </summary>
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    /// <summary>
    /// Changes the enemy's state.
    /// </summary>
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    /// <summary>
    /// Handles enemy projectile shooting when in the attack state.
    /// </summary>
    private void Shoot()
    {
        if (currentState == EnemyState.attack)
        {
            float angleStep = 360f / 10;
            float angle = 0f;

            for (int i = 0; i < 10; i++)
            {
                float projectileDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
                float projectileDirY = Mathf.Sin((angle * Mathf.PI) / 180f);
                Vector3 projectileMoveVector = new Vector3(projectileDirX, projectileDirY, 0);
                Vector2 projectileDirection = projectileMoveVector.normalized;

                GameObject projectile = Instantiate(projectile2Prefabs, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x, projectileDirection.y) * 5f;

                angle += angleStep;
            }
        }
    }

    /// <summary>
    /// Starts dialog when the player enters the enemy's trigger area.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dialogActivator != null && !dialogActivator.isDialogueFinished)
            {
                dialogActivator.StartDialog();
            }
        }
    }

    /// <summary>
    /// Marks the dialog as finished and triggers the battle sequence.
    /// </summary>
    public void OnDialogueFinished()
    {
        isDialogueFinished = true;
        if (battleManager != null)
        {
            battleManager.StartBattle();
        }
    }
}
