using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorlight : MonoBehaviour
{
    public GameObject lamp;
    public Material start;
    public Material Light;
    public GameObject textObject;
    public AudioClip clip;
    public AudioClip oclip;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lamp.GetComponent<Renderer>().material = Light;
            GetComponentInChildren<Light>().enabled = true;
            textObject.SetActive(true);
            AudioSource.PlayClipAtPoint(clip, other.GetComponentInChildren<Camera>().transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lamp.GetComponent<Renderer>().material = start;
            GetComponentInChildren<Light>().enabled = false;
            textObject.SetActive(false);
            AudioSource.PlayClipAtPoint(oclip, other.GetComponentInChildren<Camera>().transform.position);
        }
    }
}
