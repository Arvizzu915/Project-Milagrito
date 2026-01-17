using UnityEngine;

public class Gasoline : ToolAC, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;
    public float torqueStrength = .5f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    private float gasLeft = 40;

    private bool dropping = false;
    private float dropTimer = .2f;
    private float timer;
    [SerializeField] private GameObject gasMolecule;

    [SerializeField] private Lawnmower lawnmower;
    [SerializeField] private GrassManager grassManager;

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
        if (gasLeft <= 0 && grassManager.grassCount >= 0)
        {
            MissionTrigger.instance.missionFailed = true;
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
}
