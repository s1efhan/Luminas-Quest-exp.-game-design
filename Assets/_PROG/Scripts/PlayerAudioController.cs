using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public PlayerController playerController;
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource swimmingAudioSource;
    public AudioSource sneakingAudioSource;
    public AudioClip crouchingAudio;
    public AudioClip jumpingAudio;
    public AudioClip stopCrouchingAudio;
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
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        bool isRunning = playerController.isRunning;
        bool Handstand = Input.GetKeyDown(KeyCode.H);
        bool isCrouching = playerController.isCrouching;
        bool isMoving = characterController.velocity.magnitude > 0.1f;
        bool isSneaking = isMoving && isCrouching;
        bool isGrounded = characterController.isGrounded;
        bool isSwimming = playerController.isSwimming;

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
            else if (isSneaking)
            {
                if (!sneakingAudioSource.isPlaying)
                {
                    StopAllAudioSources();
                    PlaySound(sneakingAudioSource);
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

            // Crouching sounds
            if (isCrouching && !wasCrouching)
            {
                AudioSource.PlayClipAtPoint(crouchingAudio, cam.transform.position);
                timeSinceLastPlay = 0f;
            }
            else if (!isCrouching && wasCrouching)
            {
                AudioSource.PlayClipAtPoint(stopCrouchingAudio, cam.transform.position);
                timeSinceLastPlay = 0f;
            }

            // Jumping sounds
            if (Input.GetButtonDown("Jump"))
            {
                AudioSource.PlayClipAtPoint(jumpingAudio, cam.transform.position, 60);
                timeSinceLastPlay = 0f;
            }

            //HandStand Sound
            if (Handstand && !wasHandstand)
            {
                AudioSource.PlayClipAtPoint(crouchingAudio, cam.transform.position);
                timeSinceLastPlay = 0f;
            }
            else if (Handstand && wasHandstand)
            {
                AudioSource.PlayClipAtPoint(stopCrouchingAudio, cam.transform.position);
                timeSinceLastPlay = 0f;
            }
        }

        wasCrouching = isCrouching;
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
        sneakingAudioSource.Stop();
        swimmingAudioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River"))
        {
            // Spiele WaterSplash-Audio ab
            AudioSource.PlayClipAtPoint(WaterSplash, cam.transform.position);
        }
    }
}