using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    [SerializeField] private float jumpForce = 20;
    private bool grounded = true;
    private float verticalVelocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float terminalVelocity = -50f;

    public PlayerInputs inputs;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.InLevel.Enable();
    }

    private void OnEnable()
    {
        inputs.InLevel.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        inputs.InLevel.Jump.performed -= OnJump;
    }

    private void Update()
    {
        Vector2 moveInput = inputs.InLevel.Move.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;

        // Horizontal movement
        Vector3 velocity = moveDirection * speed;

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
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            verticalVelocity = jumpForce;
        }
    }
}