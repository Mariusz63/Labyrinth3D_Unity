using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController_1 : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravity = 9.81f;


    [Header("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownRange = 80.0f;

    [Header("Inputs Customisation")]
    [SerializeField] private string horizontalMoveInput = "Horizontal";
    [SerializeField] private string verticalMoveInput = "Vertical";
    [SerializeField] private string MouseXInput = "Mouse X";
    [SerializeField] private string MouseYInput = "Mouse Y";
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Footstep Sounds")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float sprintStepInterval = 0.3f;
    [SerializeField] private float velocityThreshold = 2.0f;

    //[Header("Interaction")]
    //[SerializeField] private Vector3 interactionRayPoint = default;
    //[SerializeField] private float interactionDistance = default;
    //[SerializeField] private LayerMask interactionLayer = default;
    //private Interacable currentInteractable;

    private int lastPlayedIndex = -1;
    private bool isMoving;
    private float nextStepTime;
    private Camera mainCamera;
    private float verticalRotation;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleFootsteps();
    }


    private void HandleMovement()
    {
        float verticalInput = Input.GetAxis(verticalMoveInput);
        float horizontalInput = Input.GetAxis(horizontalMoveInput);
        float speedMultiplier = Input.GetKey(sprintKey) ? sprintMultiplier : 1f;

        float verticalSpeed = verticalInput * walkSpeed * speedMultiplier;
        float horizontalSpeed = horizontalInput * walkSpeed * speedMultiplier;

        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(currentMovement * Time.deltaTime);
        isMoving = verticalInput != 0 || horizontalInput != 0;
    }

    private void HandleGravityAndJumping()
    {
        if(characterController.isGrounded)
        {
            currentMovement.y =- 0.5f;

            if(Input.GetKeyDown(jumpKey))
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {     
        float mouseXRotation = Input.GetAxis(MouseXInput) * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= Input.GetAxis(MouseYInput) * mouseSensitivity;
        verticalRotation = Math.Clamp(verticalRotation, -upDownRange, upDownRange);

        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleFootsteps()
    {
        float currentStepInterval = (Input.GetKey(sprintKey) ? sprintStepInterval : walkStepInterval);

        if(characterController.isGrounded && isMoving && Time.time> nextStepTime && characterController.velocity.magnitude > velocityThreshold)
        {
           // PlayFootstepsSounds();
            nextStepTime = Time.time+currentStepInterval;
        }   
    }

    private void PlayFootstepsSounds( )
    {
        int randomIndex;
        if(footstepSounds.Length == 1)
        {
            randomIndex = 0;
        }
        else
        {
            randomIndex = UnityEngine.Random.Range(0, footstepSounds.Length-1);
            if(randomIndex >= lastPlayedIndex)
            {
                randomIndex++;
            }
        }

        lastPlayedIndex = randomIndex;
        footstepSource.clip = footstepSounds[randomIndex];
        footstepSource.Play();
    }
}
