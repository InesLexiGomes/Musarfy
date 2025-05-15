using UnityEngine;

public class RatFSM : FSM
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float returnDistance;

    private PlayerInput player;
    private RatMovement movement;

    private Vector2 initialPosition;

    private bool requirementsFulfilled = false;
    private bool beingInteractedWith = false;
    private bool isSpotted = false;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerInput>();
        initialPosition = transform.position;
    }

    public override void DoAction(string actionID)
    {
        switch (actionID)
        {
            case ("Follow"):
                Follow();
                break;
            
            case ("Reset"):
                Reset();
                break;
            
            case ("Return"):
                Return();
                break;
            
            case ("Talk"):
                Talk();
                break;

            default:
                break;
        }
    }

    public override bool CheckCondition(string conditionID)
    {
        switch (conditionID)
        {
            case ("Interacting?"):
                return beingInteractedWith;

            case ("Done Interacting?"):
                return !beingInteractedWith;

            case ("Requirements Fulfilled?"):
                return requirementsFulfilled;

            case ("Is Spotted?"):
                return isSpotted;
            
            case ("Out of Range?"):
                return OutOfPlayerRange();
            
            default:
                return false;
        }
    }

    // Action Methods
    private void Follow()
    {
        Vector2 moveDirection = (transform.position - player.transform.position).normalized;
        movement.DoMovement(moveDirection);
    }
    private void Reset()
    {
        transform.position = initialPosition;
    }
    private void Return()
    {
        Vector2 newPosition = -(transform.position - player.transform.position).normalized * returnDistance;
        transform.position = newPosition;
    }
    private void Talk()
    {
        // Insert dialogue code here
    }

    // Condition Methods
    public void Interacting()
    {
        beingInteractedWith = true;
    }
    public void FulfillRequirements()
    {
        requirementsFulfilled = true;
    }
    public void Seen()
    {
        isSpotted = true;
    }
    private bool OutOfPlayerRange()
    {
        float distance = (transform.position - player.transform.position).magnitude;
        return distance > maxDistance;
    }
}
