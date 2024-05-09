using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool isPlayerNear = false;
    private Animator leverAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            Debug.Log("Kollision mit Lever");
            isPlayerNear = true;
            leverAnimator = other.GetComponent<Animator>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lever"))
        {
            isPlayerNear = false;
            leverAnimator = null;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Input E gedrückt");
            leverAnimator.SetTrigger("Lever");
        }
    }
}

