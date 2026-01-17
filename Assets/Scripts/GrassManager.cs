using UnityEngine;

public class GrassManager : ReseteableObjectAC
{
    public bool finalGrass = false;
    public GameObject[] grassBlocks;

    public int grassCount;
    private void OnEnable()
    {
        if (finalGrass)
        {
            foreach (GameObject grass in grassBlocks)
            {
                grass.SetActive(true);
                grassCount = 20;
            }
        }
        else
        {
            grassCount = grassBlocks.Length;
        }
    }

    private void Update()
    {
        if (!MissionManager.Instance.inMission) return;
        if (grassCount <= 0 && MissionManager.Instance.inMission)
        {
            MissionManager.Instance.FinishMission();
        }
    }

    public override void ResetObject()
    {
        foreach (var grass in grassBlocks)
        {
            grass.SetActive(true);
        }

        grassCount = grassBlocks.Length;
    }
}
