using UnityEngine;

public class FetchState : DogStateMachine
{
    private bool fetching = false, fetched = false;

    public override void EnterState(AIDog dog)
    {
        dog.agent.SetDestination(PlayerGeneral.instance.spot.position);
        dog.agent.speed = dog.walkSpeed;
        dog.staminaDrainRate = dog.walkingStaminaDrainRate;

        fetched = false;
        fetching = false;
    }

    public override void ExitState(AIDog dog)
    {

    }

    public override void UpdateState(AIDog dog)
    {
        Debug.Log("fetching");

        dog.stamina -= dog.staminaDrainRate * Time.deltaTime;
        dog.peeMeter -= dog.peeFillRate * Time.deltaTime;

        if (!fetching && Stick.instance.inHand)
        {
            StickDropped(dog);
        }

        if (fetched)
        {
            Stick.instance.transform.position = dog.mouth.position;
        }

        if (dog.stamina <= 0)
        {
            fetched = false;
            dog.ChangeState(dog.tired);
        }

        if (dog.peeMeter <= 0)
        {
            fetched = false;
            dog.ChangeState(dog.pee);
        }
    }

    public override void StateOnTriggerEnter(AIDog dog, Collider other)
    {
        if (other.gameObject.CompareTag("Stick"))
        {
            if (fetching)
            {
                fetched = true;
                dog.agent.SetDestination(PlayerGeneral.instance.spot.transform.position);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (fetched)
            {
                fetched = false;
                dog.ChangeState(dog.walk);
            }
        }
    }

    public override void StickInHand(AIDog dog)
    {
        dog.agent.SetDestination(PlayerGeneral.instance.spot.position);
        dog.agent.speed = dog.walkSpeed;

        dog.staminaDrainRate = dog.walkingStaminaDrainRate;
        fetched = false;
        fetching = false;
    }

    public override void StickDropped(AIDog dog)
    {
        dog.staminaDrainRate = dog.runningStaminaDrain;
        fetching = true;
        dog.agent.SetDestination(Stick.instance.transform.position);
        dog.agent.speed = dog.runningSpeed;
    }
}
