using System.Collections;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    //[SerializeField] private float doorOpenDelay = 5f; // Delay time before the door opens again
    //[SerializeField] private float doorCloseDelay = 5f; // Delay time before the door automatically closes
    private bool doorOpen = false;
    private AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& doorOpen == false)
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
                audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                //StartCoroutine(CloseDoorAfterDelay());
            }
        }
    }

    /*IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(doorCloseDelay);
        myDoor.Play("DoorClose", 0, 0.0f);
    }*/
}

