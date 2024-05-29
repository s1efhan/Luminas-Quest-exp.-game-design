using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControll : MonoBehaviour
{
    public float walkingSpeed = 4.0f;
    public float runningSpeed = 8.0f;
    public float jumpHeight = 8.0f;
    private bool canMove = true;
    public bool isMoving = false;
    public bool isRunning = false;
    public bool isSwimming = false;
    public bool isWalking = false;

    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isStanding = true;
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
   RaycastHit hit;
    if (Physics.Raycast(transform.position + Vector3.up * characterController.radius, Vector3.down, out hit, characterController.height * 0.6f + characterController.radius))
    {
        Vector3 surfaceNormal = hit.normal;
        float angle = Vector3.Angle(surfaceNormal, Vector3.up);

        // Wenn der Winkel zwischen der Oberflächennormalen und der vertikalen Achse (Vector3.up) kleiner als 45 Grad ist, gilt der Spieler als am Boden
        if (angle < 45f)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    else
    {
        isGrounded = false;
    }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River"))
        {
            isSwimming = true;
            // animator.SetTrigger("swim");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("River"))
        {
            isSwimming = false;
            // animator.ResetTrigger("swim"); // Setze den Trigger-Parameter "swim" zurück
        }
    }

    void HandleMovement()
    {
        if (!canMove) return;
        // Bewegungsrichtung basierend auf der relativen Richtung zur Kamera
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 movement = (forward * Input.GetAxis("Vertical")) + (right * Input.GetAxis("Horizontal"));

        // Apply gravity before checking for jump
        if (isGrounded)
        {
            moveDirection.y = 0f; // Reset y moveDirection when grounded

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpHeight; // Setzen der Sprunghöhe
                isJumping = true;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime; // Apply gravity if not grounded
        }

        // Nur die horizontalen Komponenten von moveDirection aktualisieren
        moveDirection.x = movement.normalized.x * (isRunning ? runningSpeed : walkingSpeed);
        moveDirection.z = movement.normalized.z * (isRunning ? runningSpeed : walkingSpeed);

        // Bewegung anwenden
        characterController.Move(moveDirection * Time.deltaTime);

        // Kamera- und Charakterrotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Bewegung und Zustand aktualisieren
        isMoving = (movement.x != 0 || movement.z != 0) && isGrounded && !isJumping;
        isRunning = isMoving && Input.GetKey(KeyCode.LeftShift) && !isJumping &&  isGrounded &&!isSwimming;
        isWalking = isMoving && !isRunning && !isJumping &&  isGrounded && !isSwimming;
        isStanding = !isMoving && !isRunning && !isWalking && isGrounded && !isSwimming && !isJumping;

        // Überprüfen, ob der Charakter wieder am Boden ist
        if (isGrounded && !Input.GetButtonDown("Jump"))
        {
            isJumping = false;
        }
    }
}