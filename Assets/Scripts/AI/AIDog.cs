using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof(NavMeshAgent))]
public class AIDog : MonoBehaviour
{
    public NavMeshAgent agent;

    public float walkSpeed = 2f, runningSpeed = 5f, tiredSpeed = 1f;

    public DogStateMachine stateMachine;

    public DogStateMachine currentState;

    public WalkState walk = new();
    public FetchState fetch = new();
    public SleepState sleep = new();
    public PeeState pee = new();
    public TiredState tired = new();

    public Transform[] walkingPoints;
    public int walkingPointIndex = 0;
    public Transform bed;
    public Transform peeTree;
    public Stick stick;
    public Transform mouth;

    public float sleepTimer = 25f, sleepTime = 0, stamina = 100, staminaDrainRate = 1, walkingStaminaDrainRate = 1, runningStaminaDrain = 2, peeFillRate = .2f, peeMeter = 0;

    private void Start()
    {
        peeMeter = 0;
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

    private void OnTriggerEnter(Collider other)
    {
        currentState.StateOnTriggerEnter(this, other);   
    }

    public void OnStickInHand()
    {
        currentState.StickInHand(this);
    }
}
