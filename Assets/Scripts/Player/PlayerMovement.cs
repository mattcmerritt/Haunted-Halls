using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float Sensitivity;
    public float Gravity = -9.8f;
    public float JumpSpeed;
    public float VerticalSpeed;

    public CharacterController CC;

    public Transform CameraTransform;

    private float CameraRotation;

    public bool OnGround = false;

    public CapsuleCollider PlayerCollider;

    public Vector3 StartLocation;
    public Quaternion StartAngle, StartCameraAngle;

    public SoundManager SoundManager;
    public float FallDuration = 0f;
    public float MinFallDuration;
    public float WalkMultiplier;
    public float RunDuration = 0f;
    public float MinRunDuration;
    public bool Walking, Crouching;
    public float StandHeight, CrouchHeight;
    public float CrouchFactor;
    public float MinSpeed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartLocation = transform.position;
        StartAngle = transform.rotation;
        StartCameraAngle = CameraTransform.localRotation;

        StandHeight = transform.localScale.y;
        CrouchHeight = StandHeight * CrouchFactor;
    }

    private void Update()
    {
        float rawForwardInput = Input.GetAxisRaw("Vertical");
        float rawRightInput = Input.GetAxisRaw("Horizontal");

        // normalizing input (so that putting in forward and right is not faster)
        Vector3 normalizedInput = new Vector3(rawForwardInput, 0f, rawRightInput).normalized;

        // translational movement
        float forwardInput = normalizedInput.x * MoveSpeed * Time.deltaTime;
        float rightInput = normalizedInput.z * MoveSpeed * Time.deltaTime;

        Vector3 movement = (forwardInput * transform.forward) + (rightInput * transform.right);

        // Check if player is holding walk key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunDuration = 0f;
            movement *= WalkMultiplier;
            Walking = true;
        }
        else
        {
            Walking = false;
        }

        /*
        // Check if player is holding crouch key
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!Crouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, CrouchHeight, transform.localScale.z);
                CC.Move(new Vector3(0f, -(1 - CrouchFactor) * StandHeight, 0f));
            }
            RunDuration = 0f;
            movement *= WalkMultiplier;
            Crouching = true;
            
        }
        else
        {
            if (Crouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, StandHeight, transform.localScale.z);
                CC.Move(new Vector3(0f, (1 - CrouchFactor) * StandHeight, 0f));
            }
            Crouching = false;
        }
        */
        
        // Check if the player is currently running
        if (OnGround && !Walking && !Crouching && movement.magnitude / Time.deltaTime >= MinSpeed)
        {
            RunDuration += Time.deltaTime;
        }
        else
        {
            RunDuration = 0f;
        }

        // Play sound if running for too long
        if (RunDuration >= MinRunDuration)
        {
            SoundManager.PlayFootstep();
            RunDuration = 0f;
        }

        VerticalSpeed += (Gravity * Time.deltaTime);

        // Landing Detection
        if (!OnGround)
        {
            FallDuration += Time.deltaTime;
        }

        // checking if on ground
        if (CC.isGrounded && !OnGround)
        {
            // player has hit the ground
            OnGround = true;
            // if the player fell from a high enough height, play the sound
            if (FallDuration >= MinFallDuration)
            {
                SoundManager.PlayLandingSound();   
            }
            FallDuration = 0f; // reset for next jump
        }
        else if (OnGround)
        {
            // check that there is still ground below the player (in case they walk off the edge)
            OnGround = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y + 0.1f);
        }

        // checking for jumps
        if (OnGround)
        {
            VerticalSpeed = 0f;
            // check for single jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                VerticalSpeed = JumpSpeed;
                OnGround = false;
            }
        }

        movement += (transform.up * VerticalSpeed * Time.deltaTime);

        CC.Move(movement);

        // looking around
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        CameraRotation += mouseY;
        CameraRotation = Mathf.Clamp(CameraRotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseX, 0f));
    }

    public void Reset()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // object cannot move with character controller active
        CC.enabled = false;

        transform.position = StartLocation;
        transform.rotation = StartAngle;
        CameraTransform.localRotation = StartCameraAngle;

        // reenable character controller
        CC.enabled = true;
    }
}
