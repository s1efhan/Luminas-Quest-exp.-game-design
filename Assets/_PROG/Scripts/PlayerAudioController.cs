using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource sneakingAudioSource;
    public AudioSource crouchingAudioSource;
    public AudioSource jumpingAudioSource;
    public AudioSource stopCrouchingAudioSource;

    private CharacterController characterController;
    private float timeSinceLastPlay = 0f;
    private float delay = 0.3f;
    private bool wasCrouching = false;
    private bool wasGrounded = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        bool isMoving = characterController.velocity.magnitude > 0.1f;
        bool isSneaking = isMoving && isCrouching;
        bool isGrounded = characterController.isGrounded;

        timeSinceLastPlay += Time.deltaTime;

        // Handle sounds based on movement and actions
        if (timeSinceLastPlay >= delay)
        {
            if (isSneaking)
            {
                PlaySound(sneakingAudioSource);
            }
            else if (isRunning && isGrounded)
            {
                PlaySound(runningAudioSource);
            }
            else if (isMoving && isGrounded)
            {
                PlaySound(walkingAudioSource);
            }

            // Crouching sounds
            if (isCrouching && !wasCrouching)
            {
                crouchingAudioSource.Play();
                timeSinceLastPlay = 0f;
            }
            else if (!isCrouching && wasCrouching)
            {
                stopCrouchingAudioSource.Play();
                timeSinceLastPlay = 0f;
            }

            // Jumping sounds
            if (!isGrounded && wasGrounded)
            {
                jumpingAudioSource.Play();
                timeSinceLastPlay = 0f;
            }
            else if (isGrounded && !wasGrounded)
            {
                jumpingAudioSource.Play();
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
        crouchingAudioSource.Stop();
        jumpingAudioSource.Stop();
        stopCrouchingAudioSource.Stop();

        source.Play();
        timeSinceLastPlay = 0f;
    }
}
