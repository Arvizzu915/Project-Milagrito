using UnityEngine;
using TMPro;

public class MissionFinal : MissionAC
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private GameObject grass, timerGO;
    [SerializeField] private GameObject crackHead;
    [SerializeField] private GameObject finalMission;
    public int missionsCompleted = 0;

    private bool canComplete;

    [SerializeField] private float timer = 180f;

    public float timeLeft;
    public bool running = false;

    private void Update()
    {
        if (!running) return;

        time.text = timeLeft.ToString("F0");
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            TimeFinish();
        }
    }

    protected virtual void TimeFinish()
    {
        running = false;
        MissionManager.Instance.currenMission.FailMission();
    }

    public override void CompleteMission()
    {
        if (!canComplete) return;

        timerGO.SetActive(false);
        running = false;
        MissionManager.Instance.missionsLeft--;
        MissionManager.Instance.inMission = false;

        finalMission.SetActive(true);
    }

    public override void FailMission()
    {
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
        running = true;
        canComplete = false;

        timerGO.SetActive(true);
        timeLeft = timer;
        grass.SetActive(true);

        Invoke(nameof(EnableCompletion), 0.1f);
    }

    private void EnableCompletion()
    {
        canComplete = true;
    }
}
