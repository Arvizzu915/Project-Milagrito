using UnityEngine;

public class Gasoline : ToolAC, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    private float gasLeft = 50;

    private bool dropping = false;
    private float dropTimer = .2f;
    private float timer;

    [SerializeField] private GameObject gasMolecule;

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            rb.AddForce(positionToMove * moveSpeed, ForceMode.Force);
        }
    }

    private void Update()
    {
        if (beingDragged)
        {
            positionToMove = PlayerGeneral.instance.spot.position - transform.position;
        }

        if (dropping)
        {
            gasLeft = Time.deltaTime;
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
