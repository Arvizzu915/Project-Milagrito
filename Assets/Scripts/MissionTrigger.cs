using UnityEngine;
using TMPro;

public class MissionTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private MissionAC mission;

    public bool missionStarted = false;
    public bool missionCompleted = false;
    public bool missionFailed = false;

    [SerializeField] private string[] dialogues;
    [SerializeField] private GameObject dialogueBox, missionSign, missionBox;
    [SerializeField] private TextMeshPro dialogue;
    [SerializeField] private TextMeshProUGUI missionName;

    private int dialogueIndex = -1;

    private void AdvanceDialogue()
    {
        dialogueIndex++;
        dialogue.text = dialogues[dialogueIndex];
    }

    private void StartMission()
    {
        missionName.text = mission.missionName;

        dialogueBox.SetActive(false);
        missionBox.SetActive(true);
        missionStarted = true;

        MissionManager.Instance.StartMission(mission, this);

        gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (dialogueIndex == -1)
        {
            dialogueIndex++;
            dialogueBox.SetActive(true);
            missionSign.SetActive(false);
            dialogue.text = dialogues[dialogueIndex];
        }
        else
        {
            if (dialogueIndex < dialogues.Length - 1)
            {
                AdvanceDialogue();
            }
            else
            {
                StartMission();
            }
        }
    }

    public void StopInteracting()
    {
    }
}
