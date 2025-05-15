using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyStates CurrentState { get; private set; } = EnemyStates.Patrolling;

    private Coroutine waitCoroutine;

    public void SetState(EnemyStates newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
    }

    private void Update()
    {
        Debug.Log(CurrentState);
    }
}
