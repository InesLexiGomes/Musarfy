using UnityEngine;

[CreateAssetMenu(fileName = "FSMAction", menuName = "Scriptable Objects/FSM Action")]
public class FSMAction : ScriptableObject
{
    public uint actionId;

    public void Execute(FSM stateMachine)
    {
        stateMachine.DoAction(actionId);
    }
}
