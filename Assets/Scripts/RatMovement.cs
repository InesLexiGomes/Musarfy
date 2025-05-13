using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RatMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationMultiplier;

    private Rigidbody2D rb;

    private float currentAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DoMovement(Vector2 moveDirection)
    {
        rb.linearVelocity = moveDirection * speed;
        Rotate(moveDirection);
    }

    private void Rotate(Vector2 moveDirection)
    {
        if (moveDirection !=  Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + -90;

            float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);
            currentAngle += angleDiff * Time.deltaTime * rotationMultiplier;
            transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        }
    }
}
