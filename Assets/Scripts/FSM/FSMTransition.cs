using UnityEngine;

[CreateAssetMenu(fileName = "FSMTransition", menuName = "Scriptable Objects/FSM Transition")]
public class FSMTransition : ScriptableObject
{
    public FSMState transitionState;
    public FSMCondition[] conditions;

    public void Execute(FSM stateMachine)
    {
        bool transition = false;
        if (conditions.Length == 0)
        {
            transition = true;
        }
        else
        {
            foreach (FSMCondition condition in conditions)
            {
                transition = condition.CheckCondition(stateMachine);
                // If one of them is true then the transition happens
                if (transition) break;
            }
        }

        if (transition)
        {
            stateMachine.CurrentState = transitionState;
        }
    }
}
