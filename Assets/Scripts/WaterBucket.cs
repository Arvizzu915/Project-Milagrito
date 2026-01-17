using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public float waterLeft = 100f;

    [SerializeField] private CleaningSponge sponge;

    private void Update()
    {
        if (waterLeft <= 0)
        {
            Empty();
        }
    }

    private void Empty()
    {
        if (!MissionManager.Instance.inMission) return;

        if (sponge.CheckPaintPercent() < 100 && sponge.wetness <= 0)
        {
            MissionManager.Instance.currenMission.FailMission();
        }
    }
}
