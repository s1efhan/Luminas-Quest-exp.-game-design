using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CylinderButton : MonoBehaviour
{
    public string color;
    public Material material;
    public ParticleSystem part;
    private Animator animator;
    private ParticleSystem selfpart;
    private void Start()
    {
        animator= GetComponent<Animator>();
        selfpart= GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
            selfpart.Play();    
            animator.SetTrigger("Press");
            PlayerController controller = other.GetComponent<PlayerController>();
            controller.currentColor = color;
            part.GetComponent<ParticleSystemRenderer>().material = material;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("5");
        animator.SetTrigger("Leave");
    }
}




