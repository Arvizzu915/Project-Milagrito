using UnityEngine;

public class Lawnmower : ToolAC, IInteractable
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    public bool on = false;

    public float gas = 5;

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

        if (on)
        {
            gas -= Time.deltaTime;
        }

        if (gas <= 0)
        {
            on = false;
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
            grass.GetCut();
        }
    }
}
