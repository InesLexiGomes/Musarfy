using UnityEngine;

[CreateAssetMenu(fileName = "FSMCondition", menuName = "Scriptable Objects/FSM Condition")]
public class FSMCondition : ScriptableObject
{
    public string conditionId;
    public bool CheckCondition(FSM stateMachine)
    {
        return stateMachine.CheckCondition(conditionId);
    }
}
