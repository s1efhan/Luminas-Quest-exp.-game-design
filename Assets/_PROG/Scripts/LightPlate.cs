using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Material StartMaterial;
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;
    public Material defaultMaterial;
    public AudioClip clip;
    private bool activated = false;

    private Dictionary<string, Material> materialMap = new Dictionary<string, Material>();

    void Awake()
    {
        // Materialien zu Farben hinzufügen
        materialMap.Add("red", redMaterial);
        materialMap.Add("blue", blueMaterial);
        materialMap.Add("green", greenMaterial);
        materialMap.Add("yellow", yellowMaterial);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnLeverPulled += Deactivate;
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.OnLeverPulled -= Deactivate;
        }
    }


    public Material GetMaterialByColor(string color)
    {
        if (materialMap.ContainsKey(color))
        {
            return materialMap[color];
        }
        return defaultMaterial;
    }

    public void Deactivate()
    {
        Renderer renderer = GetComponent<Renderer>();
        activated = false;
        renderer.material = StartMaterial;
    }
    void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController component not found on the player object!");
                return;
            }

            string playerColor = playerController.currentColor;
            Renderer renderer = GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogError("Renderer component not found on the light object!");
                return;
            }

            // Material der Lichtplatte ändern
            Material newMaterial = GetMaterialByColor(playerColor);
            renderer.material = newMaterial;

            // Material des Particle Systems ändern
            ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
            if (particleSystem == null)
            {
                Debug.LogError("ParticleSystem component not found in the light's children!");
                return;
            }
            ParticleSystemRenderer psRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            if (psRenderer == null)
            {
                Debug.LogError("ParticleSystemRenderer component not found on the ParticleSystem!");
                return;
            }
            psRenderer.material = newMaterial;

            Camera playerCamera = other.GetComponentInChildren<Camera>();
            if (playerCamera == null)
            {
                Debug.LogError("Camera component not found in the player's children!");
                return;
            }

            AudioSource.PlayClipAtPoint(clip, playerCamera.transform.position, 0.2f);
            particleSystem.Play();

            activated = true;
        }
    }
}
