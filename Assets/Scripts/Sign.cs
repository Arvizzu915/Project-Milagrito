using UnityEngine;

public class Sign : MonoBehaviour
{
    private Vector3 directionLook = Vector3.zero;
    public float initialDistance = 10f;

    private void Update()
    {
        directionLook = transform.position - PlayerGeneral.instance.transform.position;
        transform.LookAt(new Vector3(directionLook.x, transform.position.y, directionLook.z));


        float distance = Vector3.Distance(transform.position, PlayerGeneral.instance.transform.position);
        transform.localScale = Vector3.one * (distance) * initialDistance * .1f;
    }
}
