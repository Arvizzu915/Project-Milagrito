using UnityEngine;

public abstract class DogStateMachine
{
    public abstract void EnterState(AIDog dog);
    public abstract void UpdateState(AIDog dog);
    public abstract void ExitState(AIDog dog);
}
