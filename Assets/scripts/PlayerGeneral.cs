using UnityEngine;
using UnityEngine.UI;

public class PlayerGeneral : MonoBehaviour
{
    public static PlayerGeneral instance;

    [SerializeField] private Slider staminaSlider;
    [SerializeField] private GameObject staminaBar;
    public PlayerCamera interactSys;

    public Transform spot;

    public float stamina = 100f;
    [SerializeField] private float staminaGainRate = 50f;
    private float staminaRateRelative = 1;

    public bool pickingUp = false;
    public bool running = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        staminaRateRelative = stamina / 100;
        if (staminaRateRelative <= .3f)
        {
            staminaRateRelative = .3f;
        }

        if (staminaRateRelative >= .85)
        {
            staminaRateRelative = 1.2f;
        }

        staminaSlider.value = stamina;

        if (pickingUp && interactSys.currentTool != null)
        {
            stamina -= Time.deltaTime * interactSys.currentTool.staminaDrain;
        }

        if (stamina < 100)
        {
            stamina += Time.deltaTime * staminaGainRate * staminaRateRelative;
        }

        if (stamina >= 100)
        {
            staminaBar.SetActive(false);
            stamina = 100;
        }
        else
        {
            staminaBar.gameObject.SetActive(true);
        }

        if (stamina <= 0 && pickingUp)
        {
            interactSys.DropObject();
        }
    }
}
