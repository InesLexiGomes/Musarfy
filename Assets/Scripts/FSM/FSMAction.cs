using UnityEngine;

[CreateAssetMenu(fileName = "FSMAction", menuName = "Scriptable Objects/FSM Action")]
public abstract class FSMAction : ScriptableObject
{
    public abstract void Execute(FSM stateMachine);
}
