using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLightController : MonoBehaviour
{
    public Light lampLight; // Reference to the Light component
    public float fadeDuration = 5.0f; // Duration over which the light will fade out
    private float initialIntensity;

    void Start()
    {
        if (lampLight == null)
        {
            lampLight = GetComponent<Light>();
        }
        initialIntensity = lampLight.intensity;
        StartFading();
    }

    public void StartFading()
    {
        StartCoroutine(FadeOutLight());
    }

    private IEnumerator FadeOutLight()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            lampLight.intensity = Mathf.Lerp(initialIntensity, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        lampLight.intensity = 0f;
    }
}
