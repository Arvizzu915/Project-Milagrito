using UnityEngine;

public class MissionFinal : MissionAC
{
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject crackHead;
    [SerializeField] private GameObject finalMission;
    public int missionsCompleted = 0;

    [SerializeField] private float timer = 180f;

    private float timeLeft;
    private bool running = false;

    private void Update()
    {
        if (!running) return;

        if (missionsCompleted >= 1)
        {
            CompleteMission();
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            running = false;
            TimeFinish();
        }
    }

    protected virtual void TimeFinish()
    {
        MissionManager.Instance.currenMission.FailMission();
    }

    public override void CompleteMission()
    {
        running = false;

        MissionManager.Instance.missionsLeft--;
        MissionManager.Instance.inMission = false;

        finalMission.SetActive(true);
    }

    public override void FailMission()
    {
        running = false;
        ResetMission();
    }

    public override void ResetMission()
    {
        missionsCompleted = 0;
        timeLeft = timer;
        running = true;

        PlayerGeneral.instance.interactSys.DropObject();
        PlayerGeneral.instance.transform.position = respawn.position;

        foreach (ReseteableObjectAC obj in objects)
        {
            obj.ResetObject();
        }
    }

    public override void StartMission()
    {
        timeLeft = timer;
        running = true;
        //sonido de puerta cerrarse
        grass.SetActive(true);
    }
}
