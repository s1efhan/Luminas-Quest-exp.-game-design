using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    public float speed = 5.0f;

    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get the vertical axis input (W/S or Up/Down arrow keys)
        float verticalInput = Input.GetAxis("Vertical");

        // Determine if the character should be jogging based on the input
        bool isJogging = verticalInput > 0;

        // Update the Animator parameter
        animator.SetBool("IsJogging", isJogging);

        // Move the character forward if jogging
        if (isJogging)
        {
            Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
            transform.Translate(forwardMovement);
        }
    }
}
