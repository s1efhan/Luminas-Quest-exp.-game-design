using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public PlayerControll playerController;
    private Animator animator;
    public float speed = 5.0f;
    private bool isSwimming = false;
    private bool isMoving = false;
    private bool isWalking = false;
    private bool isRunning = false;

    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerControll>();
    }

    void Update()
    {
        bool isRunning = playerController.isRunning;
        bool isMoving = playerController.isMoving;
        bool isGrounded = playerController.isGrounded;
        bool isSwimming = playerController.isSwimming;
        bool isWalking = playerController.isWalking;

        float verticalInput = Input.GetAxis("Vertical");
        bool Jogging = verticalInput > 0;
        animator.SetBool("Jogging", isWalking);

        if (isWalking)
        {
            Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
            transform.Translate(forwardMovement);
        }
        if (isSwimming)
        {
            animator.SetBool("isSwimming", isSwimming);
        }
    }

}
