using UnityEngine;
using TMPro;

public class MissionTrigger : MonoBehaviour, IInteractable
{
    public static MissionTrigger instance;

    public bool missionStarted = false;
    public bool missionCompleted = false;
    public bool missionFailed = false;

    [SerializeField] private string[] dialogues;
    [SerializeField] private GameObject dialogueBox, missionSign, missionBox;
    [SerializeField] private TextMeshPro dialogue;

    private int dialogueIndex = -1;

    private void Awake()
    {
        instance = this;
    }

    private void AdvanceDialogue()
    {
        dialogueIndex++;
        dialogue.text = dialogues[dialogueIndex];
    }

    private void StartMission()
    {
        dialogueBox.SetActive(false);
        missionBox.SetActive(true);
        missionStarted = true;
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
