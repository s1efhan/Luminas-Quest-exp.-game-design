using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightscript : MonoBehaviour
{
    public Material mat;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {

            //Light Light = GetComponentInChildren<Light>();
            //Light.enabled = true;
            GetComponent<Renderer>().material = mat;
        }
    }
}
