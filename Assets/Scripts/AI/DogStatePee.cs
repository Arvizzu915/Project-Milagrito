using UnityEngine;

public class PeeState : DogStateMachine
{
    private bool inTree = false;

    public override void EnterState(AIDog dog)
    {
        dog.agent.speed = dog.runningSpeed;

        dog.agent.SetDestination(dog.peeTree.position);
    }

    public override void ExitState(AIDog dog)
    {
        
    }

    public override void UpdateState(AIDog dog)
    {
        Debug.Log("peeing");
        Debug.Log(inTree);

        if (inTree)
        {
            dog.peeMeter -= 25 * Time.deltaTime;
        }

        if (dog.peeMeter <= 0)
        {
            dog.ChangeState(dog.walk);
        }
    }

    public override void StateOnTriggerEnter(AIDog dog, Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            inTree = true;
        }
    }

    public override void StickInHand(AIDog dog)
    {
        
    }
}
