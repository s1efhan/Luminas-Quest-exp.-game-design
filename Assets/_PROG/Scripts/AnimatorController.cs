using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    public Animator animator;  // Attach your Animator component here via the Inspector
    private Vector3 lastPosition;
    public float minimumMovementThreshold = 0.01f;  // Minimum distance the object must move to be considered as "moving"

    void Start()
    {
        lastPosition = transform.position;  // Initialize lastPosition
        if (animator == null)
        {
            Debug.LogError("Animator component not attached to the script.", this);
            return;
        }

        // Check if Animator has an AnimatorController assigned
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator Controller is not assigned to the Animator.", animator);
            return;
        }
    }

    void Update()
    {
        // Calculate the distance moved since the last frame
        float movedDistance = Vector3.Distance(transform.position, lastPosition);

        // Check if the moved distance is greater than the threshold
        if (movedDistance > minimumMovementThreshold)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        // Update lastPosition to the current position at the end of the frame
        lastPosition = transform.position;
    }
}
