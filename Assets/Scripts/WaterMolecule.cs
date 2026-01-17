using UnityEngine;

public class WaterMolecule : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
