using UnityEngine;

public class MissionFinalFinal : MissionAC
{
    [SerializeField] private GameObject pills;
    [SerializeField] private GameObject drugman;
    [SerializeField] private NPCCrackhead drugmanScript;


    public override void CompleteMission()
    {
        drugman.SetActive(true);

        drugmanScript.StartInteraction();

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

    }
}
