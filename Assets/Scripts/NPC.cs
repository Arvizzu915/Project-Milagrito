using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour,  IInteractable
{
    [SerializeField] private string[] dialogues;
    public GameObject dialogueBox;
    [SerializeField] private TextMeshPro dialogue;

    private int dialogueIndex = -1;

    private void OnEnable()
    {
        StartInteraction();
    }

    public void SelectDialogue(int index)
    {
        dialogueBox.SetActive(true);
        dialogueIndex = index;
        dialogue.text = dialogues[dialogueIndex];
    }

    private void AdvanceDialogue()
    {
        dialogueIndex++;
        dialogue.text = dialogues[dialogueIndex];
    }

    public void Interact()
    {
        if (dialogueIndex == -1)
        {
            dialogueIndex++;
            dialogueBox.SetActive(true);
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
                FinishInteraction();
            }
        }
    }

    public virtual void StartInteraction()
    {
        dialogueIndex++;
        dialogueBox.SetActive(true);
        dialogue.text = dialogues[dialogueIndex];
    }

    public virtual void FinishInteraction()
    {
        dialogueBox.SetActive(false);
        gameObject.layer = 0;
    }

    public void StopInteracting()
    {
    }
}
