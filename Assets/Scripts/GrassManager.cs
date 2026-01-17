using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public GameObject[] grass;

    public int grassCount;
    private void Start()
    {
        grassCount = grass.Length;
    }

    private void Update()
    {
        if (grassCount <= 0)
        {
            MissionTrigger.instance.missionCompleted = true;
        }
    }
}
