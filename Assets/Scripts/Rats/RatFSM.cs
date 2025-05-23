using UnityEngine;

public class RatFSM : FSM
{
    [SerializeField] private uint requiredItemID;

    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float returnDistance;

    [SerializeField] private string[] questDialogue;
    [SerializeField] private string[] questEndDialogue;

    [SerializeField] private Sprite sprite;

    private PlayerInput player;
    private Inventory inventory;
    private RatMovement movement;
    private DialogueManager dialogueManager;

    private Vector2 initialPosition;

    private bool beingInteractedWith = false;
    private bool isSpotted = false;

    private float distanceToPlayer;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerInput>();
        inventory = player.gameObject.GetComponent<Inventory>();
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
                return CheckRequirements();

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
        else
        {
            movement.DoMovement(Vector2.zero);
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
    private bool CheckRequirements()
    {
        return inventory.CheckItemInInventory(requiredItemID);
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
        if (!CheckRequirements())
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
        if (!CheckRequirements())
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
