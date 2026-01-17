using UnityEngine;

public class MissionFinalFinal : MissionAC
{
    [SerializeField] private GameObject pills, water;
    [SerializeField] private GameObject drugman;
    [SerializeField] private GameObject drugmanRecieve;
    [SerializeField] private NPCCrackhead drugmanScript;

    public bool recievedPill, recievedWater;

    private void Update()
    {
        if (recievedPill && recievedWater)
        {
            CompleteMission();
        }
    }
        

    public override void CompleteMission()
    {
        drugmanRecieve.SetActive(false);

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
        recievedPill = false;
        recievedWater = false;

        garrafon.instance.watering = true;

        pills.SetActive(true);
        pills.GetComponent<Pills>().missionScript = this;
        water.GetComponent<WaterGlass>().missionScript = this;

        drugmanRecieve.SetActive(true);
    }
}
