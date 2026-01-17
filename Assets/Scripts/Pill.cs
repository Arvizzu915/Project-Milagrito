using UnityEngine;

public class Pill : MonoBehaviour
{
    public CapsuleCollider col;

    private void OnEnable()
    {
        Invoke(nameof(ColEnable), .3f);
    }

    private void ColEnable()
    {
        col.enabled = true;
    }
}
