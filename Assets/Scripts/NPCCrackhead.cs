using System.Collections;
using UnityEngine;

public class NPCCrackhead : NPC
{
    public IEnumerator ShutUp()
    {
        yield return new WaitForSeconds(20f);
        dialogueBox.SetActive(false);
        Destroy(dialogueBox);
    }
}
