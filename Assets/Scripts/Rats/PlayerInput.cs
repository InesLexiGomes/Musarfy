using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private RatMovement movement;

    private void Start()
    {
        movement = GetComponent<RatMovement>();
    }

    private void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.DoMovement(moveDirection);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RatFSM ratOther = collision.gameObject.GetComponentInParent<RatFSM>();
        if (ratOther != null && Input.GetKeyDown(KeyCode.E))
        {
            ratOther.StartInteracting();
        }
    }
}
