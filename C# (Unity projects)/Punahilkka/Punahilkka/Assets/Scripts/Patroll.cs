using UnityEngine;

/// <summary>
/// Handles enemy patrol behavior between waypoints, including direction changes.
/// </summary>
public class Patroll : MonoBehaviour
{
    public float speed; // Patrol speed
    public Transform[] wpoints; // Waypoints for patrol
    private int waypointIndex; // Current waypoint index

    /// <summary>
    /// Moves the enemy between waypoints and changes direction upon reaching them.
    /// </summary>
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wpoints[waypointIndex].position,
            speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wpoints[waypointIndex].position) < 0.1f)
        {
            if (waypointIndex < wpoints.Length - 1)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex = 0;
            }

            ChangeDirection(waypointIndex);
        }
    }

    /// <summary>
    /// Adjusts the enemy's rotation to face the next waypoint.
    /// </summary>
    /// <param name="wpIndex">The index of the next waypoint.</param>
    void ChangeDirection(int wpIndex)
    {
        Vector2 direction = wpoints[wpIndex].transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
