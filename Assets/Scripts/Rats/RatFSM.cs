using UnityEngine;

public class RatFSM : FSM
{
    public override void DoAction(string actionID)
    {
        switch (actionID)
        {
            
            default:
                return;
        }
    }

    public override bool CheckCondition(string conditionID)
    {
        switch (conditionID)
        {

            default:
                return false;
        }
    }
}
