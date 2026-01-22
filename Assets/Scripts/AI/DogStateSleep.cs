using UnityEngine;

public class SleepState : DogStateMachine
{
    public override void EnterState(AIDog dog)
    {
        dog.agent.speed = 0;

        dog.sleepTime = Time.time;
    }

    public override void ExitState(AIDog dog)
    {

    }

    public override void UpdateState(AIDog dog)
    {
        Debug.Log("sleeping");

        if (Time.time - dog.sleepTime > dog.sleepTimer)
        {
            dog.ChangeState(dog.walk);
        }
    }

    public override void StateOnTriggerEnter(AIDog dog, Collider other)
    {
    }

}
