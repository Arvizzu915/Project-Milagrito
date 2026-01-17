using UnityEngine;

public class Pills : ToolAC, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;
    public float torqueStrength = 2f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    private bool closed = true;

    [SerializeField] private GameObject cap, pills;

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
    }

    public override void Use()
    {
        if (closed)
        {
            OpenPills();
        }
        else
        {
            DropPills();
        }
    }

    private void OpenPills()
    {
        closed = false;
        cap.transform.parent = null;
        cap.AddComponent<Rigidbody>();
    }

    private void DropPills()
    {
        Instantiate(pills, transform.position, Quaternion.identity);
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
