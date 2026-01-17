using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneral : MonoBehaviour
{
    public static PlayerGeneral instance;

    [SerializeField] private Slider staminaSlider;
    [SerializeField] private GameObject staminaBar;
    [SerializeField] private PlayerCamera interactSys;

    public Transform spot;

    public float stamina = 100f;
    [SerializeField] private float staminaGainRate = 50f;

    public bool pickingUp = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        staminaSlider.value = stamina;

        if (pickingUp && interactSys.currentTool != null)
        {
            stamina -= Time.deltaTime * interactSys.currentTool.staminaDrain;
        }
        else
        {
            if (stamina < 100)
            {
                stamina += Time.deltaTime * staminaGainRate;
            }
        }

        if (stamina >= 100)
        {
            staminaBar.SetActive(false);
            stamina = 100;
        }
        else
        {
            staminaSlider.gameObject.SetActive(true);
        }

        if (stamina <= 0 && pickingUp)
        {
            interactSys.DropObject();
        }
    }
}
