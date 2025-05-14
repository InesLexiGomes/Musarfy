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
}
