using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit
    public float mouseSensitivity = 2f; // Mausempfindlichkeit

    private float xRotation = 0f; // Rotation um die X-Achse (Nicken)

    void Update()
    {
        // Bewegung mit WASD-Tasten
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Rotation mit der Maus
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Begrenze die Rotation um die X-Achse

        transform.Rotate(Vector3.up * mouseX); // Rotation um die Y-Achse
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotation der Kamera um die X-Achse
    }
}