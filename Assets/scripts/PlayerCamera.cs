using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMov;

    [SerializeField] float sensitivity;

    private Vector2 mouseInput;

    private float pitch;
    public float upperLimit = 80f;
    public float lowerLimit = 80f;


    private PlayerInputs playerControls;
    private InputAction lookAction;

    [Header("Raycast Settings")]
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private LayerMask hitLayers = Physics.DefaultRaycastLayers;

    private IInteractable currentObject;

    private RaycastHit currentHit; // store the latest raycast hit
    private bool hasHit;

    void Start()
    {
        playerControls = playerMov.inputs;

        lookAction = playerControls.InLevel.Look;

        // Enable the input action
        lookAction.Enable();

        playerControls.InLevel.InteractMain.started += InteractMain;
        playerControls.InLevel.InteractMain.canceled += StopInteracting;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        // Disable the input action
        lookAction.Disable();

        playerControls.InLevel.InteractMain.started -= InteractMain;
        playerControls.InLevel.InteractMain.canceled -= StopInteracting;
    }

    private void Update()
    {
        mouseInput = playerControls.InLevel.Look.ReadValue<Vector2>();

        playerMov.transform.Rotate(Vector3.up, mouseInput.x * sensitivity * Time.deltaTime);

        pitch -= mouseInput.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, lowerLimit, upperLimit);

        transform.localEulerAngles = new Vector3(pitch, transform.localEulerAngles.y, 0);

        // --- Raycast Forward ---
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, hitLayers))
        {
            currentHit = hit;
            hasHit = true;

            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        }
        else
        {
            hasHit = false;
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        }
    }

    private void InteractMain(InputAction.CallbackContext context)
    {
        if (!hasHit) return;

        IInteractable interactable = currentHit.collider.GetComponent<IInteractable>();

        if (interactable == null) return;
        currentObject = interactable;
        interactable.Interact();
    }

    private void StopInteracting(InputAction.CallbackContext context)
    {
        if (currentObject == null) return;

        currentObject.StopInteracting();
    }    
}
