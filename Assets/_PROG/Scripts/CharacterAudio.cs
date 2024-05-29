using UnityEngine;
public class CharacterAudio : MonoBehaviour
{
public PlayerControll playerController;
public AudioSource walkingAudioSource;
public AudioSource runningAudioSource;
public AudioSource swimmingAudioSource;
public AudioClip WaterSplash;
public CharacterController characterController;
private float timeSinceLastPlay = 0f;
private float delay = 0.1f;
private Camera cam;
void Start()
{
    cam = GetComponentInChildren<Camera>();
    playerController = GetComponent<PlayerControll>();
}
void Update()
{
bool isRunning = playerController.isRunning;
bool isMoving = playerController.isMoving;
bool isGrounded = playerController.isGrounded;
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
else if (isRunning && /*isGrounded && */ isMoving)
{
if (!runningAudioSource.isPlaying)
{
StopAllAudioSources();
PlaySound(runningAudioSource);
}
}
else if (isMoving /* && isGrounded */)
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