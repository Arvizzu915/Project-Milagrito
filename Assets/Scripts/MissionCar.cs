using UnityEngine;

public class MissionCar : MissionAC
{
    [SerializeField] private GameObject sponge;
    [SerializeField] private GameObject carman;
    [SerializeField] private NPCCarman carmanScrpit;


    public override void CompleteMission()
    {
        carman.SetActive(true);

        carmanScrpit.SelectDialogue(0);

        carmanScrpit.StartCoroutine(nameof(carmanScrpit.ShutUp));

        MissionManager.Instance.missionsLeft--;
        MissionManager.Instance.inMission = false;
    }

    public override void FailMission()
    {
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
        MissionManager.Instance.index = 2;
        //sonido de puerta cerrarse
        sponge.SetActive(true);
    }
}
