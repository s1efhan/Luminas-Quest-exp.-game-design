using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource sneakingAudioSource;
    public AudioClip crouchingAudio;
    public AudioClip jumpingAudio;
    public AudioClip stopCrouchingAudio;

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
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        bool Handstand = Input.GetKeyDown(KeyCode.H);
        bool isMoving = characterController.velocity.magnitude > 0.1f;
        bool isSneaking = isMoving && isCrouching;
        bool isGrounded = characterController.isGrounded;

        timeSinceLastPlay += Time.deltaTime;

        // Handle sounds based on movement and actions
        if (timeSinceLastPlay >= delay)
        {
            if (isSneaking)
            {
                if(!sneakingAudioSource.isPlaying)
                {
                    PlaySound(sneakingAudioSource);
                }
            }
            else if (isRunning && isGrounded && isMoving)
            {
                if (!runningAudioSource.isPlaying)
                {
                    PlaySound(runningAudioSource);
                }
            }
            else if (isMoving && isGrounded)
            {
                if(!walkingAudioSource.isPlaying)
                {
                    PlaySound(walkingAudioSource);
                }
            }
            else
            {
                walkingAudioSource.Stop();
                runningAudioSource.Stop();
                sneakingAudioSource.Stop();
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
        walkingAudioSource.Stop();
        runningAudioSource.Stop();
        sneakingAudioSource.Stop();

        source.Play();
        timeSinceLastPlay = 0f;
    }
}
