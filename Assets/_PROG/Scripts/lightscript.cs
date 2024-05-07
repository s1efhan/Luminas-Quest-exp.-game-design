using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightscript : MonoBehaviour
{
    public Material mat;
    public AudioClip clip;
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            if (other.CompareTag("Player"))
            {
                activated= true;
                GetComponent<Renderer>().material = mat;
                Camera Cameramain = other.GetComponentInChildren<Camera>();
                AudioSource.PlayClipAtPoint(clip, Cameramain.transform.position, 0.2f);
                this.GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }
}
