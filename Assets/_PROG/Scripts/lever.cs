using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool isPlayerNear = false;
    public Animator leverAnimator;
    public AudioClip clip;
    public Camera playerCamera;
    private float timeSinceLastPlay = 0f;
    private float delay = 0.5f;

    void Awake()
    {
        leverAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            playerCamera = other.GetComponentInChildren<Camera>();
}
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    IEnumerator DelayedAction(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        EventManager.Instance.LeverPulled();
    }

    void Update()
    {
        timeSinceLastPlay += Time.deltaTime;
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && timeSinceLastPlay >= delay)
        {
            leverAnimator.SetTrigger("Lever");
            AudioSource.PlayClipAtPoint(clip, playerCamera.transform.position, 0.2f);
            StartCoroutine(DelayedAction(0.5f));
            GetComponentInChildren<ParticleSystem>().Play();
   
        }
    }
}
