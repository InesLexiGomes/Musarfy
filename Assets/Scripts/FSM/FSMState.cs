using UnityEngine;

[CreateAssetMenu(fileName = "FSMState", menuName = "Scriptable Objects/FSM State")]
public class FSMState : ScriptableObject
{
    public FSMTransition[] transitions;
    public FSMAction action;

    public void Execute(FSM stateMachine)
    {
        // Verify action exists
        if (action != null)
            // Execute action
            action.Execute(stateMachine);

        foreach (FSMTransition transition in transitions)
        {
            // Execute transition and define priority
            transition.Execute(stateMachine);
        }
    }
}
