using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float walkingSpeed = 8.0f;
    public float runningSpeed = 12.0f;
    public float crouchSpeed = 4.0f; // Crouching wieder hinzugefügt
    public float jumpSpeed = 8.0f;
    public float gravity = 15.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.5f;
    public float lookXLimit = 45.0f;
    public float rotationSpeed = 100.0f;
    public Animator animator;
    public ParticleSystem part;
    public AudioClip fart;

    public bool isMoving = false;
    public bool isCrouching = false; // Crouching wieder hinzugefügt
    public bool isRunning = false;
    public bool isSwimming = false;
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isStanding = true; // Default ist true
    public bool ground = true;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float timeSinceLastPlay = 0f;
    private float delay = 0.5f;
    private bool canMove = true;
    public string currentColor = "yellow";
    private bool handstand = false;

    private GameObject[] torchObjRefs;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Finde alle Objekte mit dem Tag "Torch" und speichere sie im Array
        torchObjRefs = GameObject.FindGameObjectsWithTag("Torch");
    }

    private void Update()
    {
        HandleMovement();
        HandleActions();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River"))
        {
            // Deaktiviere alle Torch-Objekte
            for (int i = 0; i < torchObjRefs.Length; i++)
            {
                if (torchObjRefs[i] != null)
                {
                    torchObjRefs[i].SetActive(false);
                }
            }
            isSwimming = true;
            animator.SetTrigger("swim");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("River"))
        {
            // Aktiviere alle zuvor deaktivierten Torch-Objekte wieder
            for (int i = 0; i < torchObjRefs.Length; i++)
            {
                if (torchObjRefs[i] != null)
                {
                    torchObjRefs[i].SetActive(true);
                }
            }
            isSwimming = false;
            animator.ResetTrigger("swim"); // Setze den Trigger-Parameter "swim" zurück
        }
    }

    void HandleMovement()
    {
        if (!canMove) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        isMoving = moveHorizontal != 0 || moveVertical != 0;
        isWalking = isMoving && !isRunning && !isJumping && !isSwimming;
        isRunning = isMoving && Input.GetKey(KeyCode.LeftShift) && !isJumping && !isSwimming;
        isStanding = !isMoving && !isRunning && !isWalking && !isSwimming; 

        float speed = isRunning ? runningSpeed : (isCrouching ? crouchSpeed : walkingSpeed);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement != Vector3.zero)
        {
            movement = transform.TransformDirection(movement.normalized) * speed;
            animator.SetBool("walking", isWalking);
            animator.SetBool("running", isRunning);
            animator.SetBool("crouching", isCrouching); 
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetInteger("horizontal", 0);
            animator.SetBool("running", false);
            animator.SetBool("crouching", isCrouching);
        }
        if (characterController.isGrounded)
        {
            isJumping = false;

        }
        else { isJumping = true; }
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            animator.SetTrigger("jump");
            moveDirection.y = jumpSpeed;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetBool("handstand", handstand);
            handstand = handstand ? false : true;
        }

        moveDirection.x = movement.x;
        moveDirection.z = movement.z;
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        // Player camera rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleActions()
    {
        timeSinceLastPlay += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F) && timeSinceLastPlay >= delay)
        {
            part.Play();
            timeSinceLastPlay = 0f;
            AudioSource.PlayClipAtPoint(fart, playerCamera.transform.position);
        }
    }
}