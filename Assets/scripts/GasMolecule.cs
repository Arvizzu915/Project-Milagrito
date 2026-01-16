using UnityEngine;

public class GasMolecule : MonoBehaviour
{
    public SphereCollider col;

    private void OnEnable()
    {
        Invoke(nameof(ColEnable), .3f);
    }

    private void ColEnable()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Lawnmower lawnmower))
        {
            lawnmower.gas += .3f;
        }

        gameObject.SetActive(false);
    }
}
