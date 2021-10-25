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

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartLocation = transform.position;
        StartAngle = transform.rotation;
        StartCameraAngle = CameraTransform.localRotation;
    }

    private void Update()
    {
        // translational movement
        float forwardInput = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        float rightInput = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

        Vector3 movement = (forwardInput * transform.forward) + (rightInput * transform.right);
        VerticalSpeed += (Gravity * Time.deltaTime);

        // checking if on ground
        if (CC.isGrounded && !OnGround)
        {
            // player has hit the ground
            OnGround = true;
        }
        else if (OnGround)
        {
            // check that there is still ground below the player (in case they walk off the edge)
            OnGround = Physics.Raycast(transform.position, Vector3.down, PlayerCollider.height + 0.1f);
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
