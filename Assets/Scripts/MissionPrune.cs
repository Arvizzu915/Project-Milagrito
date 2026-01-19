using System.Collections.Generic;
using UnityEngine;

public class MissionPrune : MissionAC
{
    [SerializeField] private GameObject blockingDoor;
    [SerializeField] private GameObject oldman;
    [SerializeField] private NPCOldman oldmanScript;
    [SerializeField] private Transform oldmanSpawn;
    [SerializeField] private GameObject gasolineExtra;


    public override void CompleteMission()
    {
        gasolineExtra.SetActive(true);

        oldmanScript.SelectDialogue(0);

        oldmanScript.StartCoroutine(nameof(oldmanScript.ShutUp));

        MissionManager.Instance.missionsLeft--;
        MissionManager.Instance.inMission = false;
    }

    public override void FailMission()
    {
        Debug.Log("fail");

        ResetMission();
    }

    public override void ResetMission()
    {
        PlayerGeneral.instance.interactSys.DropObject();
        PlayerGeneral.instance.transform.position = respawn.position;

        foreach (ReseteableObjectAC obj in objects)
        {
            obj.ResetObject();
        }
    }

    public override void StartMission()
    {
        MissionManager.Instance.index = 1;
        oldman.transform.position = oldmanSpawn.position;
        oldman.SetActive(true);
        blockingDoor.SetActive(false);
    }
}
