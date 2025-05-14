using UnityEngine;

[CreateAssetMenu(fileName = "FSMCondition", menuName = "Scriptable Objects/FSM Condition")]
public abstract class FSMCondition : ScriptableObject
{
    public abstract bool CheckCondition(FSM stateMachine);
}
