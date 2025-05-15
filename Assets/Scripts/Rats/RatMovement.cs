using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RatMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private float headRotationMultiplier;

    [SerializeField] private float maxHeadTurnAngle = 90;

    [SerializeField] private GameObject head;

    private Rigidbody2D rb;

    private float currentAngleBody;
    private float currentAngleHead;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DoMovement(Vector2 moveDirection)
    {
        Rotate(moveDirection);
        moveDirection = moveDirection.normalized;
        rb.linearVelocity = transform.up * moveDirection.magnitude * speed;
    }

    private void Rotate(Vector2 moveDirection)
    {
        if (moveDirection !=  Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + -90;

            float angleDiffBody = Mathf.DeltaAngle(currentAngleBody, targetAngle);
            currentAngleBody += angleDiffBody * Time.deltaTime * rotationMultiplier;
            transform.rotation = Quaternion.AngleAxis(currentAngleBody, Vector3.forward);

            float relativeHeadAngle = Mathf.DeltaAngle(currentAngleBody, currentAngleHead);
            float targetRelativeAngle = Mathf.DeltaAngle(currentAngleBody, targetAngle);
            targetRelativeAngle = Mathf.Clamp(targetRelativeAngle, -maxHeadTurnAngle, maxHeadTurnAngle);

            float angleDiffHead = targetRelativeAngle - relativeHeadAngle;
            currentAngleHead += angleDiffHead * Time.deltaTime * headRotationMultiplier;
            head.transform.rotation = Quaternion.AngleAxis(currentAngleHead, Vector3.forward);
        }
    }
}
