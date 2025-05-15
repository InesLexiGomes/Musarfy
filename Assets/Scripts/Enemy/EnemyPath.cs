using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class EnemyPath : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float waitTime = 1.5f;

    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Vector2 direction;
    private EnemyStateManager stateManager;

    void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
        rb = GetComponent<Rigidbody2D>();

        if (waypoints.Length > 0)
            SetDirectionToCurrentWaypoint();
    }

    private void FixedUpdate()
    {
        if (waypoints.Length == 0 || stateManager.CurrentState != EnemyStates.Patrolling)
        {
            StopMovement();
            return;
        }

        HandlePatrol();
    }

    private void HandlePatrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        direction = (targetWaypoint.position - transform.position).normalized;

        ApplyMovement();

        if (IsNearWaypoint(targetWaypoint))
        {
            StartCoroutine(PauseBeforeNextWaypoint());
        }
    }

    private void ApplyMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        if (stateManager.CurrentState != EnemyStates.Waiting)
            Flip(); 
    }

    private void StopMovement() => rb.linearVelocity = Vector2.zero;

    private void SetDirectionToCurrentWaypoint() =>
        direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;

    private bool IsNearWaypoint(Transform waypoint) =>
    Vector2.Distance(transform.position, waypoint.position) < 0.1f;

    private void Flip()
    {
        // If direction.x is positive, face right (0° rotation), else face left (180° rotation)
        float zRotation;
        if (direction.x >= 0)
            zRotation = 0f;
        else
            zRotation = 180f;

        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }

    private IEnumerator PauseBeforeNextWaypoint()
    {
        stateManager.SetState(EnemyStates.Waiting);

        // Wait for the specified seconds
        yield return new WaitForSeconds(waitTime);

        // Move to next waypoint in sequence
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        SetDirectionToCurrentWaypoint();

        stateManager.SetState(EnemyStates.Patrolling);
    }
}
