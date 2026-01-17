using UnityEngine;

public class GasMolecule : MonoBehaviour
{
    public SphereCollider col;

    private void OnEnable()
    {
        Invoke(nameof(ColEnable), .24f);

        Invoke(nameof(Disable), 3f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void ColEnable()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Lawnmower lawnmower))
        {
            lawnmower.gas += 1;
        }

        gameObject.SetActive(false);
    }
}
