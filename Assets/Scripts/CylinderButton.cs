using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderButton : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimator = null;
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // For Animator Controller-based animation
            buttonAnimator.Play("ButtonPress", 0, 0.0f);

            // Execute button action or trigger event
            Debug.Log("Button pressed by player!");
        }
    }
}




