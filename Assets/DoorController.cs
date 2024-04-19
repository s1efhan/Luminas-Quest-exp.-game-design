using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false; // Zustand der Tür (geöffnet oder geschlossen)
    public Transform door; // Referenz auf das Türobjekt
    public Transform button; // Referenz auf das Knopfobjekt
public Camera mainCamera;
    void Update()
    {
        // Wenn der Spieler auf den Knopf zielt und einen Mausklick macht, soll sich die Tür togglen
        if (IsPointingAtButton() && Input.GetMouseButtonDown(0))
        {
            ToggleDoor();
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen; // Wechsle den Zustand der Tür

        // Rotiere die Tür entsprechend ihres Zustands
        if (isOpen)
        {
            door.transform.Rotate(Vector3.up, 90f);
        }
        else
        {
            door.transform.Rotate(Vector3.up, -90f);
        }
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