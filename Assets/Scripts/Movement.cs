using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private string inputMoveName = "Move";

    /*[Header("Sprint Settings")]
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float sprintStaminaCostPerSecond;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashStaminaCost;
    [SerializeField] private float dashCooldown;

    [Header("Gravity Settings")]
    [SerializeField] private float gravityMultiplier = 2f;*/

    /*[Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;*/

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private Vector2 footstepPitchRange = new Vector2(0.9f, 1.1f);
    [SerializeField] private float footstepInterval = 0.4f;
    [SerializeField] private float sprintFootstepInterval = 0.25f;

    private CharacterController controller;
    private Animator animator;
    private InputAction input;

    /*private PlayerStamina stamina;
    private bool wasSprinting = false;
    private float lastDashTime = -999f;
    private bool isDashing = false;*/
    private float verticalVelocity = 0f;
    private float footstepTimer = 0f;


    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        input = InputSystem.actions.FindAction(inputMoveName);
    }

    private void Update()
    {
        //ApplyGravity();

        /*if (!CanMove())
        {
            animator.SetFloat("VelocityX", 0f, 0.1f, Time.deltaTime);
            animator.SetFloat("VelocityZ", 0f, 0.1f, Time.deltaTime);
            animator.SetFloat("MoveMagnitude", 0f);
            return;
        }*/

        HandleMovement();
    }

    /*private void ApplyGravity()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f; // Small constant keeps the controller grounded
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        controller.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }*/

    private void HandleMovement()
    {
        Vector2 moveInput = input.ReadValue<Vector2>();
        Vector3 inputTo3D = new Vector3(-moveInput.y, 0, moveInput.x);

        if (inputTo3D.magnitude >= 0.1f)
        {
            controller.Move(inputTo3D * moveSpeed * Time.deltaTime);
        }

        Vector3 localMove = transform.InverseTransformDirection(inputTo3D);

        animator.SetFloat("VelocityX", localMove.x, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityZ", localMove.z, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveMagnitude", inputTo3D.magnitude);

        if (inputTo3D.magnitude >= 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                footstepTimer = footstepInterval;
                /*if (footstepSounds.Length > 0 && AudioManager.Instance != null)
                    AudioManager.Instance.PlaySound(
                        footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)],
                        UnityEngine.Random.Range(footstepPitchRange.x, footstepPitchRange.y)
                    );*/
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
}