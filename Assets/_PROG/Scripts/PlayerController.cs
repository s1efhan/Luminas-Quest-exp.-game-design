using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;

    public float walkingSpeed = 8.0f;
    public float runningSpeed = 12.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 15.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.5f;
    public float lookXLimit = 45.0f;
    public float rotationSpeed = 100.0f; // Geschwindigkeit der Drehung des Modells

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isRunning = false;

    [HideInInspector]
    public bool canMove = true;
    public Animator animator;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Check if the player is running
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) : 0;

        // Get the input vector from keyboard or analog stick
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera's rotation
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movement = transform.TransformDirection(movement);
        movement *= currentSpeed;

        // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            animator.SetTrigger("jump");
            moveDirection.y = jumpSpeed;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Moving
        moveDirection.x = movement.x;
        moveDirection.z = movement.z;
        characterController.Move(moveDirection * Time.deltaTime);

        if(isRunning)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("running", false);
            animator.SetBool("walking", false);
        }

        // Player rotation based on mouse input
        if (canMove)
        {

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Rotating model with Q and E, but keeping camera angle
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }
}
