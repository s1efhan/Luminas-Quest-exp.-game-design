using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomAnimator : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Reference to the Post-Processing Volume component
    public float minIntensity = 1f; // Minimum bloom intensity
    public float maxIntensity = 10f; // Maximum bloom intensity
    public float speed = 1f; // Speed of the animation

    private Bloom bloom; // Reference to the Bloom effect

    void Start()
    {
        // Get the Bloom effect from the Post-Processing Volume component
        postProcessVolume.profile.TryGetSettings(out bloom);
    }

    void Update()
    {
        // Calculate bloom intensity variation based on time
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * speed, 1f));

        // Apply intensity variation to the Bloom effect
        if (bloom != null)
        {
            bloom.intensity.value = intensity;
        }
    }
}