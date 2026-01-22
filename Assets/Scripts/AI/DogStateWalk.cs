using System;
using UnityEngine;

public class WalkState : DogStateMachine
{
    private int pointIndex = 0;

    public override void EnterState(AIDog dog)
    {
        dog.staminaDrainRate = dog.walkingStaminaDrainRate;

        dog.agent.speed = dog.walkSpeed;

        dog.agent.SetDestination(GetClosestPoint(dog.walkingPoints, dog.transform.position).position);
    }

    public override void ExitState(AIDog dog)
    {
        
    }

    public override void UpdateState(AIDog dog)
    {
        Debug.Log("walking");

        dog.stamina -= dog.staminaDrainRate * Time.deltaTime;
        dog.peeMeter += dog.peeFillRate * Time.deltaTime;

        if (dog.stamina <= 0)
        {
            dog.ChangeState(dog.tired);
        }

        if (dog.peeMeter >= 30)
        {
            dog.ChangeState(dog.pee);
        }
    }

    public override void StateOnTriggerEnter(AIDog dog, Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            pointIndex = (pointIndex + 1) % dog.walkingPoints.Length;

            dog.agent.SetDestination(dog.walkingPoints[pointIndex].position);
        }
    }

    private Transform GetClosestPoint(Transform[] points, Vector3 currentPosition)
    {
        pointIndex = 0;

        int pointIndexCurrent = 0;

        Transform closestPoint = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform point in points)
        {
            Vector3 directionToTarget = point.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestPoint = point;
                pointIndex = pointIndexCurrent;
            }

            pointIndexCurrent++;
        }

        return closestPoint;
    }

    public override void StickInHand(AIDog dog)
    {
        if (dog.stamina > 0)
        {
            dog.ChangeState(dog.fetch);
        }
    }
}
