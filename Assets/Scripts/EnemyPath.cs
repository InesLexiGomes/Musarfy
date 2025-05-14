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
    [SerializeField]
    private bool isHorizontal = true;

    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Vector2 direction;
    private EnemyStateManager stateManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager = GetComponent<EnemyStateManager>();
        rb = GetComponent<Rigidbody2D>();

        if (waypoints.Length > 0)
            SetDirectionToCurrentWaypoint();
    }

    // Update is called once per frame
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

        isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        ApplyMovement(isHorizontal);

        if (IsNearWaypoint(targetWaypoint))
        {
            StartCoroutine(PauseBeforeNextWaypoint());
        }
    }

    private void ApplyMovement(bool isMoveHorizontal)
    {
        Vector2 velocity = rb.linearVelocity;

        if (isMoveHorizontal)
        {
            velocity.x = (stateManager.CurrentState == EnemyStates.Waiting) ? 0 : direction.x * speed;
            rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocityY);
        }
        else
        {
            velocity.y = (stateManager.CurrentState == EnemyStates.Waiting) ? 0 : direction.y * speed;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, velocity.y);
        }

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
        float yRotation;
        if (direction.x >= 0)
            yRotation = 0f;
        else
            yRotation = 180f;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private IEnumerator PauseBeforeNextWaypoint()
    {
        stateManager.SetState(EnemyStates.Waiting);

        // Wait for the specified seconds
        yield return new WaitForSeconds(waitTime);

        // Move to next waypoint in sequence
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        SetDirectionToCurrentWaypoint();
        //Flip();

        stateManager.SetState(EnemyStates.Patrolling);
    }
}
