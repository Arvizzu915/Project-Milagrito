using UnityEngine;

public class Lawnmower : ToolAC, IInteractable
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;
    public float torqueStrength = .5f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    public bool on = false;

    public float gas = 5, initGas = 5;
    private float gasDrain = 1f;

    public Gasoline gasoline = null;
    public GrassManager grassManager = null;

    private SoundObject myAudioSource;

    private void Awake()
    {
        TryGetComponent(out myAudioSource);
    }

    private void OnEnable()
    {
        gas = initGas;
    }

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            rb.AddForce(positionToMove * moveSpeed, ForceMode.Force);

            Vector3 desiredUp = Vector3.up;
            Vector3 torqueVector = Vector3.Cross(transform.up, desiredUp) * torqueStrength;

            rb.AddTorque(torqueVector);
        }
    }

    private void Update()
    {
        if (beingDragged)
        {
            positionToMove = PlayerGeneral.instance.spot.position - transform.position;
        }

        if (on)
        {
            myAudioSource.PlaySoundIndex(2, 0.1f);
            gas -= Time.deltaTime * gasDrain;
        }

        if (gas <= 0 && on != false)
        {
            //le agregue el on == false para que solo se vacie el tanque 1 vez, pero si da errores quitalo
            on = false;
            EmptyTank();
        }
    }

    private void Fail()
    {

        MissionManager.Instance.currenMission.FailMission();
    }

    private void EmptyTank()
    {
        myAudioSource.CancelCurrentSound();
        myAudioSource.PlaySoundIndex(1, 0.5f);
        if (!MissionManager.Instance.inMission) return;

        if (gasoline.gasLeft <= 0 && grassManager.grassCount > 0)
        {
            Fail();
        }
    }

    public void Interact()
    {
        beingDragged = true;
        rb.useGravity = false;
    }

    public void StopInteracting()
    {
        beingDragged = false;
        rb.useGravity = true;
    }

    public override void Use()
    {
        if (!on)
        {
            if (gas >= 0)
            {
                myAudioSource.PlaySoundIndex(0, 0.5f);
                on = true;
            }
        }
        else
        {
            on = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!on) return;

        if (other.gameObject.TryGetComponent(out Grass grass))
        {
            myAudioSource.CancelCurrentSound();
            myAudioSource.PlaySoundIndex(3, 0.7f);
            grass.GetCut();
            grassManager.grassCount--;
        }
    }

    public override void ResetUsing()
    {
        on = false;
    }
}
