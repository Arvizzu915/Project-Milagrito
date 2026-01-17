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
            }
        }

        grassCount = grassBlocks.Length;
    }

    private void Update()
    {
        if (grassCount <= 0 && MissionManager.Instance.inMission)
        {
            if (finalGrass)
            {
                MissionFinal missionFinal = FindFirstObjectByType<MissionFinal>();
                missionFinal.missionsCompleted++;
            }
            MissionManager.Instance.currenMission.CompleteMission();
            MissionManager.Instance.inMission = false;
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
