using System;
using UnityEngine;

public class TiredState : DogStateMachine
{
    public override void EnterState(AIDog dog)
    {
        dog.agent.speed = dog.tiredSpeed;

        dog.agent.SetDestination(dog.bed.position);
    }

    public override void ExitState(AIDog dog)
    {
        
    }

    public override void UpdateState(AIDog dog)
    {
        Debug.Log("tired");
    }

    public override void StateOnTriggerEnter(AIDog dog, Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            dog.ChangeState(dog.sleep);
        }
    }
}
