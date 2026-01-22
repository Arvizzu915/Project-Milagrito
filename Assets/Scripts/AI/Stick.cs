using UnityEngine;

public class Stick : MonoBehaviour, IInteractable
{
    public static Stick instance;
    public AIDog dog;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    public bool inHand = false;

    private void Awake()
    {
        instance = this;
    }

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
        
        inHand = true;
        beingDragged = true;
        rb.useGravity = false;
    }

    public void StopInteracting()
    {
        inHand = false;
        beingDragged = false;
        rb.useGravity = true;
    }
}
