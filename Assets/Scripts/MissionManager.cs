using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public  class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [SerializeField] private List<MissionTrigger> missions = new();
    public MissionAC currenMission;
    public MissionTrigger currentMissionTrigger = null;

    public bool inMission;

    public int missionsLeft = 5;

    private void Awake()
    {
        Instance = this;
    }

    public void StartMission(MissionAC mission, MissionTrigger trigger)
    {
        inMission = true;

        foreach (MissionTrigger m in missions)
        {
            m.gameObject.SetActive(false);
        }

        missions.Remove(trigger);

        currenMission = mission;
        currenMission.StartMission();
    }

    public void FinishMission()
    {
        currenMission.CompleteMission();

        missions.Remove(currentMissionTrigger);

        foreach (MissionTrigger m in missions)
        {
            m.gameObject.SetActive(true);
        }
    }
}
