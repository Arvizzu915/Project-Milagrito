using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    
    void Update()
    {
        Vector3 directionAwayFromCamera = transform.position - Camera.main.transform.position;

        // Rotate the text to look in that direction
        transform.rotation = Quaternion.LookRotation(directionAwayFromCamera);
    }
}
