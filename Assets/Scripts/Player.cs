using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 20f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Rigidbody myRigidbody = gameObject.GetComponent<Rigidbody>();

        // Erzeuge einen Bewegungsvektor basierend auf Eingaben
        Vector3 movement = new Vector3(h, 0f, v);

        // Transformiere den Bewegungsvektor relativ zur Kamera
        Vector3 cameraRelativeMovement = Camera.main.transform.TransformDirection(movement);

        // Bewege das Rigidbody ohne die y-Komponente zu berücksichtigen, um Höhenänderungen zu vermeiden
        cameraRelativeMovement.y = 0;

        // Wende die Bewegung an
        myRigidbody.AddForce(cameraRelativeMovement * speed);
    }
}