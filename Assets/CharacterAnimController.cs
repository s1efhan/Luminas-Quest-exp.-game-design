using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    private Animator animator;
    public bool Swimming = false;
    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Get the vertical axis input (W/S or Up/Down arrow keys)
        float verticalInput = Input.GetAxis("Vertical");

        // Determine if the character should be jogging based on the input
        bool Jogging = verticalInput > 0;
        bool Running = Input.GetKey(KeyCode.LeftShift);

        animator.SetBool("isRunning", Running);
        animator.SetBool("isJogging", Jogging);
    }
}
