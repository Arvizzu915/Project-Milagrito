using UnityEngine;

public class Stick : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            rb.AddForce(positionToMove * moveSpeed, ForceMode.Force);

            Vector3 desiredUp = Vector3.up;
            Vector3 torqueVector = Vector3.Cross(transform.up, desiredUp);

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
