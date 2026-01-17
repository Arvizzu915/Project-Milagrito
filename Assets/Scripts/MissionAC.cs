using System.Collections.Generic;
using UnityEngine;

public abstract class MissionAC : MonoBehaviour
{
    public string missionName;
    public List<ReseteableObjectAC> objects = new();

    public int missionIndex = 0;
    public Transform respawn;

    public bool completed = false;

    public abstract void StartMission();

    public abstract void CompleteMission();

    public abstract void FailMission();

    public abstract void ResetMission();
}
