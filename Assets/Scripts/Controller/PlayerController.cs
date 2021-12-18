using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which player moves")]
    public float moveSpeed = 2f;
    [Tooltip("The speed at which player looks around(Calculated in degrees)")]
    public float lookSpeed = 60f;
    [Tooltip("The power with which player jumps")]
    public float jumpPower=8f;
    [Tooltip("The strength of gravity")]
    public float gravity = 9.81f;

    [Header("Jump timing")]
    public float jumpTimeLeniency = 0.1f;
    public float timeToStopBeingLenient = 0;

    [Header("Required References")]
    [Tooltip("The player shooter script that fires projectiles.")]
    public Shooter playerShooter;

    private CharacterController controller;
    private InputManager inputManager;
    bool DoubleJumpAvailable=false;


    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        SetupCharacterController();
        SetUpInputManager();
    }

    private void SetupCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a character controller on the same game object!");
        }
    }

    void SetUpInputManager()
    {
        inputManager = InputManager.instance;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    Vector3 moveDirection;

    void ProcessMovement()
    {
        // Get the input from the input manager
        float leftRightInput = inputManager.horizontalMoveAxis;
        float forwardBackwardInput = inputManager.verticalMoveAxis;
        bool jumpPressed = inputManager.jumpPressed;

        // Handle the control of the player while it is on the ground
        if (controller.isGrounded)
        {
            DoubleJumpAvailable = true;
            timeToStopBeingLenient = Time.time + jumpTimeLeniency;
            // Set the movement direction to be the recieved input, set y to 0 since we are on the ground
            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);

            // Set the move direction in relation to the transform
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * moveSpeed;

            if (jumpPressed)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection = new Vector3(leftRightInput * moveSpeed, moveDirection.y, forwardBackwardInput * moveSpeed);
            moveDirection = transform.TransformDirection(moveDirection);
            if (jumpPressed&&Time.time<timeToStopBeingLenient)
            {
                moveDirection.y = jumpPower;
            }
           else if (jumpPressed && DoubleJumpAvailable)
            {
                moveDirection.y = jumpPower;
                DoubleJumpAvailable = false;
            }
        }


        moveDirection.y -= gravity*Time.deltaTime;

        if (controller.isGrounded&&moveDirection.y<0)
        {
            moveDirection.y = -0.3f;
        }

        controller.Move(moveDirection * Time.deltaTime);

    }

    void ProcessRotation()
    {
        float horizontalLookInput = inputManager.horizontalLookAxis;
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x, playerRotation.y + horizontalLookInput * lookSpeed * Time.deltaTime
            , playerRotation.z));
    }

}
