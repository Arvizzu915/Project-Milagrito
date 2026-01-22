using UnityEngine;

public abstract class DogStateMachine
{
    public abstract void EnterState(AIDog dog);
    public abstract void UpdateState(AIDog dog);
    public abstract void ExitState(AIDog dog);

    public virtual void StickInHand(AIDog dog)
    {

    }

    public virtual void StickDropped(AIDog dog)
    {

    }

    public abstract void StateOnTriggerEnter(AIDog dog, Collider other);
}
