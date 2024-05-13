using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorlight : MonoBehaviour
{
    public GameObject lamp;
    public Material start;
    public Material Light;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lamp.GetComponent<Renderer>().material = Light;
            GetComponentInChildren<Light>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lamp.GetComponent<Renderer>().material = start;
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}
