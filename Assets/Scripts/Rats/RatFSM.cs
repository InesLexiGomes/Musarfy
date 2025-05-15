using UnityEngine;

public class RatFSM : FSM
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float returnDistance;

    [SerializeField] private string[] questDialogue;
    [SerializeField] private string[] questEndDialogue;

    [SerializeField] private Sprite sprite;

    private PlayerInput player;
    private RatMovement movement;
    private DialogueManager dialogueManager;

    private Vector2 initialPosition;

    private bool requirementsFulfilled = false;
    private bool beingInteractedWith = false;
    private bool isSpotted = false;

    private float distanceToPlayer;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerInput>();
        movement = GetComponent<RatMovement>();
        initialPosition = transform.position;
        dialogueManager = FindAnyObjectByType<DialogueManager>();
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
        if ((player.transform.position - transform.position).magnitude > minDistance)
        {
            Vector2 moveDirection = player.transform.position - transform.position;
            movement.DoMovement(moveDirection);
        }
    }
    private void Reset()
    {
        transform.position = initialPosition;
    }
    private void Return()
    {
        Vector2 newPosition = player.transform.position -(player.transform.position- transform.position).normalized * returnDistance;
        transform.position = newPosition;
    }
    private void Talk()
    {
        dialogueManager.EnableDialogueUI(this);
    }

    // Condition Methods
    public void StartInteracting()
    {
        beingInteractedWith = true;
    }
    public void StopInteracting()
    {
        beingInteractedWith = false;
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
        distanceToPlayer = (player.transform.position - transform.position).magnitude;
        return distanceToPlayer > maxDistance;
    }

    public string GetQuestDialogue(uint currentDialogueID)
    {
        if (!requirementsFulfilled)
        {
            return questDialogue[currentDialogueID];
        }
        else
        {
            return questEndDialogue[currentDialogueID];
        }
    }

    public int GetQuestDialogueLenght()
    {
        if (!requirementsFulfilled)
        {
            return questDialogue.Length;
        }
        else
        {
            return questEndDialogue.Length;
        }
    }

    public Sprite GetNPCSprite()
    {
        return sprite;
    }
}
