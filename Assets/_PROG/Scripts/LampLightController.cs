using System.Collections;
using UnityEngine;

public class LampLightController : MonoBehaviour
{
    public Light lampLight; // Reference to the Light component
    public ParticleSystem flameParticleSystem; // Reference to the ParticleSystem component
    public float fadeDuration = 5.0f; // Duration over which the light and particle system will fade out
    private float initialIntensity;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MainModule mainModule;
    private float initialEmissionRate;
    private float initialStartSize;

    void Start()
    {
        if (lampLight == null)
        {
            lampLight = GetComponent<Light>();
        }

        if (flameParticleSystem == null)
        {
            flameParticleSystem = GetComponent<ParticleSystem>();
        }

        initialIntensity = lampLight.intensity;
        emissionModule = flameParticleSystem.emission;
        mainModule = flameParticleSystem.main;
        initialEmissionRate = emissionModule.rateOverTime.constant;
        initialStartSize = mainModule.startSize.constant;

        StartFading();
    }

    public void StartFading()
    {
        StartCoroutine(FadeOutLight());
        StartCoroutine(FadeOutFlame());
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

    private IEnumerator FadeOutFlame()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newEmissionRate = Mathf.Lerp(initialEmissionRate, 0f, elapsedTime / fadeDuration);
            float newStartSize = Mathf.Lerp(initialStartSize, 0f, elapsedTime / fadeDuration);
            emissionModule.rateOverTime = newEmissionRate;
            mainModule.startSize = newStartSize;
            yield return null;
        }

        emissionModule.rateOverTime = 0f;
        mainModule.startSize = 0f;
    }
}
