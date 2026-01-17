using UnityEngine;

public class Pill : MonoBehaviour
{
    public MissionFinalFinal missionScript;
    public CapsuleCollider col;

    private void OnEnable()
    {
        Invoke(nameof(ColEnable), .3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("drugman"))
        {
            missionScript.recievedPill = true;
            gameObject.SetActive(false);
        }
    }

    private void ColEnable()
    {
        col.enabled = true;
    }
}
