using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Lawnmower : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;

    public bool beingDragged = false;

    private Vector3 positionToMove = Vector3.zero;

    private void Update()
    {
        if (beingDragged)
        {
            positionToMove = PlayerGeneral.instance.spot.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            if (beingDragged)
            {
                rb.AddForce(positionToMove * moveSpeed, ForceMode.Force);
            }   
        }
    }

    public void Interact()
    {
        Debug.Log(gameObject.name);

        beingDragged = true;
    }

    public void StopInteracting()
    {
        beingDragged = false;
    }
}
