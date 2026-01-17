using UnityEngine;

public class FF : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(dd), 10f);
    }

    private void dd()
    {
        gameObject.SetActive(false);
    }
}
