using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    public float speed = 5.0f;
    private bool Swimming = false;

    void Start()
    {
        // Get the Animator component attached to the character
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        bool Jogging = verticalInput > 0;
        animator.SetBool("Jogging", Jogging);

        if (Jogging)
        {
            Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
            transform.Translate(forwardMovement);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("River"))
        {
            bool Swimming = true;
            animator.SetBool("isSwimming", Swimming);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("River"))
        {
            bool Swimming = false;
            animator.SetBool("isSwimming", Swimming);
        }
    }
}
