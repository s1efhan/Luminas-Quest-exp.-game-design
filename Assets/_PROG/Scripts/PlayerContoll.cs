using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControll : MonoBehaviour
{
    public float walkingSpeed = 4.0f;
    public float runningSpeed = 8.0f;
    private bool canMove = true;
    public bool isRunning = false;
    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 15.0f;
    public float lookSpeed = 2.5f;
    public float lookXLimit = 45.0f;
    public float rotationSpeed = 100.0f;
    private float rotationX = 0;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        if (!canMove) return;

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runningSpeed : walkingSpeed;

        // Bewegungsrichtung basierend auf der relativen Richtung zur Kamera
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 movement = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

        // Only update the horizontal components of moveDirection
        moveDirection.x = movement.normalized.x * speed;
        moveDirection.z = movement.normalized.z * speed;

        // Apply gravity
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Apply movement
        characterController.Move(moveDirection * Time.deltaTime);

        // Kamera- und Charakterrotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
