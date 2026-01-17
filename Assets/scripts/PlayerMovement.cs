using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float normalSpeed;
    public float currentSpeed = 0;

    [SerializeField] private float jumpForce = 20;
    private bool grounded = true;
    private float verticalVelocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -50f;

    private bool running = false;
    [SerializeField] private float runningStaminaDrain = 5f;

    public PlayerInputs inputs;

    private void Awake()
    {
        currentSpeed = normalSpeed;

        inputs = new PlayerInputs();
        inputs.InLevel.Enable();
    }

    private void OnEnable()
    {
        inputs.InLevel.Jump.performed += OnJump;
        inputs.InLevel.Run.started += Run;
        inputs.InLevel.Run.canceled += StopRunningInput;
    }

    private void OnDisable()
    {
        inputs.InLevel.Jump.performed -= OnJump;
        inputs.InLevel.Run.started -= Run;
        inputs.InLevel.Run.canceled -= StopRunningInput;
    }

    private void Update()
    {
        grounded = controller.isGrounded;

        Vector2 moveInput = inputs.InLevel.Move.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Horizontal movement
        Vector3 velocity = moveDirection * currentSpeed;

        // Apply gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, terminalVelocity);
        }

        // Add vertical component
        velocity.y = verticalVelocity;

        // Apply movement once
        controller.Move(velocity * Time.deltaTime);

        if (PlayerGeneral.instance.stamina <= 0)
        {
            StopRunning();
        }

        if (running)
        {
            PlayerGeneral.instance.running = true;
            PlayerGeneral.instance.stamina -= runningStaminaDrain * Time.deltaTime;
        }
    }
    private void Run(InputAction.CallbackContext ctx)
    {
        if (PlayerGeneral.instance.stamina >= 0 && !running)
        {
            running = true;
            currentSpeed = normalSpeed * 1.8f;
        }
    }

    private void StopRunningInput(InputAction.CallbackContext ctx)
    {
        StopRunning();
    }

    private void StopRunning()
    {
        running = false;
        currentSpeed = normalSpeed;
        PlayerGeneral.instance.running = false;
    }


    private void OnJump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            grounded = false;
            verticalVelocity = jumpForce;
        }
    }
}