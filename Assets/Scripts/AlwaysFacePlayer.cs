using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 direction = transform.position - PlayerGeneral.instance.transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}