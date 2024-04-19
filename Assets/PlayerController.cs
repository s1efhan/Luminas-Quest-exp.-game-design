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

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool isRunning = false;

    [HideInInspector]
    public bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = true;
    }
    public GameObject bonbonPrefab; // Referenz auf das Bonbon-Prefab

void SpawnNewBonbon()
{
    // Zufällige Position innerhalb des Umkreises von 25 Einheiten
    Vector3 randomPosition = transform.position + new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));

    // Überprüfen, ob die zufällige Position auf dem Boden liegt
    RaycastHit hit;
    if (Physics.Raycast(randomPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity))
    {
        // Bonbon-Position mit Offset über dem Boden
        randomPosition = hit.point + Vector3.up * 1;
    }

    // Neues Bonbon an der zufälligen Position spawnen
    Instantiate(bonbonPrefab, randomPosition, Quaternion.identity);
}
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Bonbon"))
    {
        Debug.Log("Kollision mit Bonbon erkannt!");
        scoreManager.IncreaseCount();
        Destroy(other.gameObject); // Optional: Die eingesammelte Kugel entfernen
        SpawnNewBonbon();
    }
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
        movement = Vector3.ClampMagnitude(movement, 1.0f); // Normalize the movement vector
        movement = transform.TransformDirection(movement); // Transform the movement vector to world space
        movement *= currentSpeed; // Apply the desired speed

        // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = moveDirection.y;
        }

        // Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Moving
        moveDirection.x = movement.x;
        moveDirection.z = movement.z;
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
    
}