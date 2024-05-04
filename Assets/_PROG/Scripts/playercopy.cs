using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    public float walkingSpeed = 8.0f;
    public float runningSpeed = 12.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 15.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private bool isRunning = false;
    public bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (canMove)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
            float speed = isRunning ? runningSpeed : walkingSpeed;

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            Vector3 movement = forward * moveVertical + right * moveHorizontal;
            movement.y = 0; // Ensure moveDirection does not affect the y-axis

            if (movement.magnitude > 0.1f) // Only rotate if there is sufficient movement
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * speed);
            }

            moveDirection = movement * speed;

            if (characterController.isGrounded)
            {
                moveDirection.y = Input.GetButton("Jump") ? jumpSpeed : 0;
            }
            else
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
