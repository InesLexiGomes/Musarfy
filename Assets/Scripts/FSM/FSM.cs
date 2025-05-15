using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    [SerializeField] private FSMTransition[] anyStateTransitions;
    [SerializeField] private FSMState initialState;
    public FSMState CurrentState;

    protected virtual void Start()
    {
        CurrentState = initialState;
    }

    protected virtual void Update()
    {
        CurrentState.Execute(this);

        if (anyStateTransitions.Length > 0)
        {
            foreach (FSMTransition transition in anyStateTransitions)
            {
                transition.Execute(this);
            }
        }
    }

    public abstract void DoAction(string actionID);
    public abstract bool CheckCondition(string conditionID);
}
