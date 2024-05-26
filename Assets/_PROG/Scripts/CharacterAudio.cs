using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    public CharacterAnimController playerController;
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource swimmingAudioSource;
    public AudioClip WaterSplash;

    private CharacterController characterController;
    private float timeSinceLastPlay = 0f;
    private float delay = 0.1f;
    private bool wasCrouching = false;
    private bool wasGrounded = true;
    private bool wasHandstand = false;
    private Camera cam;

    void Start()
    {
        playerController = GetComponent<CharacterAnimController>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        bool isRunning = playerController.Running;
        bool Handstand = Input.GetKeyDown(KeyCode.H);
        bool isMoving = characterController.velocity.magnitude > 0.1f;
        bool isGrounded = characterController.isGrounded;
        bool isSwimming = playerController.Swimming;

        timeSinceLastPlay += Time.deltaTime;

        if (timeSinceLastPlay >= delay)
        {
            if (isSwimming)
            {
                if (!swimmingAudioSource.isPlaying)
                {
                    StopAllAudioSources();
                    PlaySound(swimmingAudioSource);
                }
            }
            else if (isRunning && isGrounded && isMoving)
            {
                if (!runningAudioSource.isPlaying)
                {
                    StopAllAudioSources();
                    PlaySound(runningAudioSource);
                }
            }
            else if (isMoving && isGrounded)
            {
                if (!walkingAudioSource.isPlaying)
                {
                    StopAllAudioSources();
                    PlaySound(walkingAudioSource);
                }
            }
            else
            {
                StopAllAudioSources();
            }
        }
        wasGrounded = isGrounded;
    }

    void PlaySound(AudioSource source)
    {
        source.Play();
        timeSinceLastPlay = 0f;
    }

    void StopAllAudioSources()
    {
        walkingAudioSource.Stop();
        runningAudioSource.Stop();
        swimmingAudioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River"))
        {
            AudioSource.PlayClipAtPoint(WaterSplash, cam.transform.position, 0.1f);
        }
    }
}