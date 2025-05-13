using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{
    [SerializeField] private FSMTransition[] anyStateTransitions;
    [SerializeField] private FSMState initialState;
    public FSMState CurrentState;

    [Header("NavMesh Variables")]
    [SerializeField] private int Stage1ID;
    [SerializeField] private int Stage2ID;
    [SerializeField] private int RestaurantID;
    [SerializeField] private int GreenSpaceID;

    [SerializeField] private GameObject panicStationObject;

    public Transform[] exits;

    private NavMeshAgent navMeshAgent;
    private bool currentStage1 = true;

    [Header("Agent Variables")]
    [SerializeField] private float speed = 40;
    public int defaultSaturation = 10;
    [SerializeField] private float saturationRecoverySpeed = 10f;
    [SerializeField] private float saturationDepletionSpeed = 1f;
    public int defaultEnergy = 10;
    [SerializeField] private float energyRecoverySpeed = 4f;
    [SerializeField] private float energyDepletionSpeed = 2f;

    public int minHunger;
    public int minTired;
    public int fulfillmentRequirement;

    private bool isFleeing = false;
    public bool IsFleeing => isFleeing;

    
    public float Saturation { get; private set; }
    public float Energy { get; private set; }
    public bool Panicking { get; private set; }
    public bool SatDown { get; private set; }
    public bool HitByExplosion1 { get; private set; }
    public bool HitByExplosion2 { get; private set; }
    public bool HitByExplosion3 { get; private set; }
    public bool HitByFire { get; private set; }

    private bool fsmDisabled = false;

    private bool pendingPanic = false;

    private bool hasBeenParalyzed = false;


    private Seat currentSeat;
    public Seat CurrentSeat
    {
        get => currentSeat;
        set => currentSeat = value;
    }

    private bool dead = false;

    private void Start()
    {
        Energy = defaultEnergy;
        Saturation = defaultSaturation;
        CurrentState = initialState;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
    }

    private void FixedUpdate()
    {
        if (!dead && !fsmDisabled)
        {
            CurrentState.Execute(this);

            if (anyStateTransitions.Length > 0)
            {
                foreach (FSMTransition transition in anyStateTransitions)
                {
                    transition.Execute(this);
                }
            }

            UseEnergy();
            UseSaturation();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DeathZone>() != null)
            HitByExplosion1 = true;

        if (other.GetComponent<ParalyzedZone>() != null)
        {
            // Prevent re-paralyzing if already paralyzed before
            if (!hasBeenParalyzed)
            {
                Paralyze();
                hasBeenParalyzed = true; // Mark that the agent has been paralyzed
            }
        }

        if (other.GetComponent<PanicZone>() != null)
        {
            if (navMeshAgent.speed == 0) // If agent is currently paralyzed
            {
                pendingPanic = true; // Save panic for later
            }
            else
            {
                StartPanicking(); // Panic immediately
            }
        }

        if (other.GetComponent<FireSpread>() != null)
            HitByFire = true;
    }





    private void UseEnergy()
    {
        Energy -= saturationDepletionSpeed * Time.fixedDeltaTime;
        if (Energy <= 0) Energy = 0;
    }

    private void UseSaturation()
    {
        Saturation -= energyDepletionSpeed * Time.fixedDeltaTime;
        if (Saturation <= 0) Saturation = 0;
    }

    public void Rest()
    {
        Energy += saturationRecoverySpeed * Time.fixedDeltaTime;
        SatDown = false;
    }

    public void Eat()
    {
        Debug.Log("Eating");
        Saturation += energyRecoverySpeed * Time.fixedDeltaTime;
        SatDown = false;
    }

    public void PathfindToStage()
    {
        if (currentSeat == null)
        {
            if (currentStage1)
                TrySitInSeatGroup(Stage1ID);
            else
                TrySitInSeatGroup(Stage2ID);
        }
        else if (currentStage1 && currentSeat.groupID != Stage1ID)
            TrySitInSeatGroup(Stage1ID);
        else if (!currentStage1 && currentSeat.groupID != Stage2ID)
            TrySitInSeatGroup(Stage2ID);
    }

    public void PathfindToRestaurant()
    {
        if (currentSeat == null || currentSeat.groupID != RestaurantID)
        {
            TrySitInSeatGroup(RestaurantID);
        }
        else if(currentSeat != null && currentSeat.isOccupied && currentSeat.groupID == RestaurantID)
            SatDown = true;
    }

    public void PathfindToGreenSpace()
    {
        if (currentSeat == null || currentSeat.groupID != GreenSpaceID)
        {
            TemporarySeatController.Instance.HandleTemporarySeatRequest(gameObject);
        }
        else if (currentSeat != null && currentSeat.isOccupied && currentSeat.groupID == GreenSpaceID)
            SatDown = true;
    }

    public void PathfindToExit()
    {
        if (navMeshAgent.speed == 0)
        {
            navMeshAgent.speed = speed / 2;
        }

        Transform exitChosen = null;
        float shortestDistance = float.MaxValue;

        foreach (Transform exit in exits)
        {
            float distance = (transform.position - exit.position).magnitude;
            if (distance < shortestDistance)
            {
                exitChosen = exit;
                shortestDistance = distance;
            }
        }

        if (exitChosen != null)
        {
            navMeshAgent.SetDestination(exitChosen.position);
        }
    }

    public void SwitchStage()
    {
        currentStage1 = !currentStage1;
    }

    public void StartPanicking()
    {
        if (Panicking) return; // Already panicking

        Panicking = true;
        isFleeing = true;
        fsmDisabled = true;

        navMeshAgent.speed = speed * 2;

        if (currentSeat != null)
        {
            SeatManager.Instance.ReleaseSeat(currentSeat);
            currentSeat = null;
        }

        if (navMeshAgent.hasPath)
        {
            navMeshAgent.ResetPath();
        }

        PathfindToExit();

        if (panicStationObject != null)
        {
            panicStationObject.SetActive(true);
        }

        HitByExplosion3 = false;
    }




    public void Paralyze()
    {
        navMeshAgent.speed = 0;
        HitByExplosion2 = false;

        Invoke(nameof(RecoverFromParalysis), 2f);
    }

    private void RecoverFromParalysis()
    {
        navMeshAgent.speed = speed/2;

        if (pendingPanic)
        {
            StartPanicking();
            pendingPanic = false;
        }
    }



    public void Die()
    {
        dead = true;
        Debug.Log("Dead");
        Destroy(gameObject);
    }

    private void TrySitInSeatGroup(int groupID)
    {
        if (currentSeat != null)
        {
            SeatManager.Instance.ReleaseSeat(currentSeat);
            currentSeat = null;
            Debug.Log("Leaving seat.");
            return;
        }

        Seat availableSeat = SeatManager.Instance.FindAvailableSeatInGroup(groupID);

        if (availableSeat != null)
        {
            navMeshAgent.SetDestination(availableSeat.transform.position);
            currentSeat = availableSeat;
            availableSeat.isReserved = true;
            Debug.Log("Going to seat in group " + groupID);
        }
        else
        {
            Debug.Log("No available seats in group " + groupID);
        }
    }
}
