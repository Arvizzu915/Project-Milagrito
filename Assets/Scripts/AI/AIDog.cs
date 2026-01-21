using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class AIDog : MonoBehaviour
{
    public NavMeshAgent agent;

    public float walkSpeed = 2f, runningSpeed = 5f, tiredSpeed = 1f;

    public DogStateMachine stateMachine;

    public DogStateMachine currentState;

    public WalkState walk;
    public FetchState fetch;
    public SleepState sleep;
    public PeeState pee;

    public Transform[] walkingPoints;
    public int walkingPointIndex = 0;
    public Transform bed;
    public Transform[] peePoints;
    public Stick stick;

    public float sleepTimer = 25f, stamina = 100, staminaDrainRate = 1;

    private void Start()
    {
        currentState = walk;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(DogStateMachine newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
