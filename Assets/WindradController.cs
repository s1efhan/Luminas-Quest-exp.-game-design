using UnityEngine;

public class WindradController : MonoBehaviour
{
    public Transform button; // Referenz auf das Knopfobjekt
    public float drehgeschwindigkeit = 150f; // Drehgeschwindigkeit in Grad pro Sekunde
    public bool istAktiv = false; // Steuert, ob das Windrad dreht oder nicht
    public Camera mainCamera;
    void Update()
    {

        if (IsPointingAtButton() && Input.GetMouseButtonDown(0))
        {
            ToggleWindrad();
        }
        if (istAktiv)
        {
            // Rotiere das Windrad um die Z-Achse
            transform.Rotate(Vector3.forward * drehgeschwindigkeit * Time.deltaTime);
        }
    }

    void ToggleWindrad()
    {
        istAktiv = !istAktiv; // Wechsle den Zustand des Windrads
    }

    bool IsPointingAtButton()
    {
        // Erstelle einen Raycast von der Mausposition in die Kamerarichtung
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Überprüfe, ob der Raycast den Knopf trifft
        if (Physics.Raycast(ray, out hit))
        {
            // Wenn der getroffene Collider dem Knopf entspricht, return true
            if (hit.collider.gameObject == button.gameObject)
            {
                return true;
            }
        }

        // Andernfalls return false
        return false;
    }
}