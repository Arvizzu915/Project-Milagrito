using UnityEngine;

public class garrafon : MonoBehaviour
{
    public static garrafon instance;

    public Transform spot;
    public GameObject water;
    private float waterRate = .4f;
    public bool watering = false;


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Water), 0f, waterRate);
    }

    private void Water()
    {
        if (watering)
        {
            Instantiate(water, spot.transform.position, Quaternion.identity);
        }
    }
}
