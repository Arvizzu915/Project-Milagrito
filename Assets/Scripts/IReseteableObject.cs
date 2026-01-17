using UnityEngine;

public class ReseteableObjectAC : MonoBehaviour
{
    Vector3 pos = Vector3.zero;
    Quaternion rot = Quaternion.identity;

    private void Start()
    {
        Debug.Log("start");
        FindFirstObjectByType<PlayerCamera>().objects.Add(this);
        MissionManager.Instance.currenMission.objects.Add(this);

        pos = transform.position;
        rot = transform.rotation;
    }

    public virtual void ResetObject()
    {
        gameObject.SetActive(false);

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (TryGetComponent<ToolAC>(out ToolAC tool))
        {
            tool.ResetUsing();
        }

        transform.position = pos;
        transform.rotation = rot;
        gameObject.SetActive(true);
    }
}
