using UnityEngine;

public class Gasoline : ToolAC, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;
    public float torqueStrength = .5f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    public float gasLeft = 40, gasLeftInit;

    private bool dropping = false;
    private float dropTimer = .2f;
    private float timer;
    [SerializeField] private GameObject gasMolecule;

    public Lawnmower lawnmower;
    public GrassManager grassManager;

    private void OnEnable()
    {
        gasLeft = gasLeftInit;
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
        if (gasLeft <= 0)
        {
            GasEmpty();
        }

        if (beingDragged)
        {
            positionToMove = PlayerGeneral.instance.spot.position - transform.position;
        }

        if (dropping && gasLeft >= 0)
        {
            DropGas();
        }
    }

    private void GasEmpty()
    {
        if (!MissionManager.Instance.inMission) return;

        if (lawnmower.gas <= 0 && grassManager.grassCount > 0)
        {
            MissionManager.Instance.currenMission.FailMission();
        }
    }

    private void DropGas()
    {
        if (Time.time - timer >= dropTimer)
        {
            timer = Time.time;
            Instantiate(gasMolecule, transform.position, transform.rotation);
            gasLeft--;
        }
    }

    public override void Use()
    {
        if (dropping)
        {
            dropping = false;
        }
        else
        {
            dropping = true;
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

    public override void ResetUsing()
    {
        dropping = false;
    }
}
