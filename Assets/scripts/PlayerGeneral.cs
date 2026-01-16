using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public static PlayerGeneral instance;

    public Transform spot;

    private void Awake()
    {
        instance = this;
    }
}
