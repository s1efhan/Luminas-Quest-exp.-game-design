using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Material StartMaterial;
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;
    public Material Tr_redMaterial;
    public Material Tr_blueMaterial;
    public Material Tr_greenMaterial;
    public Material Tr_yellowMaterial;
    public Material defaultMaterial;
    public AudioClip clip;
    private bool activated = false;
    public GameObject prefab;

    private Dictionary<string, Material> materialMap = new Dictionary<string, Material>();
    private Dictionary<Material, Material> TrMap = new Dictionary<Material, Material>();

    void Awake()
    {
        // Materialien zu Farben hinzufügen
        materialMap.Add("red", redMaterial);
        materialMap.Add("blue", blueMaterial);
        materialMap.Add("green", greenMaterial);
        materialMap.Add("yellow", yellowMaterial);

        TrMap.Add(redMaterial, Tr_redMaterial);
        TrMap.Add(blueMaterial, Tr_blueMaterial);
        TrMap.Add(yellowMaterial, Tr_yellowMaterial);
        TrMap.Add(greenMaterial, Tr_greenMaterial);
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
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision(other);
        }
        else if (other.CompareTag("Ball"))
        {
            HandleBallCollision(other);
        }
    }

    private void HandlePlayerCollision(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (!activated && !controller.isCrouching)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController component not found on the player object!");
                return;
            }

            string playerColor = playerController.currentColor;
            Material newMaterial = GetMaterialByColor(playerColor);
            ChangeColor(newMaterial, other);

            if (controller.isRunning)
            {
                Vector3 spawnPosition = this.transform.position;
                spawnPosition.y -= 1; // Position 1 Einheit unter dem Referenzobjekt
                prefab.GetComponent<Renderer>().material = TrMap[newMaterial];
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }

            activated = true;
        }
        else if (activated && controller.isCrouching)
        {
            Deactivate();
        }
    }

    private void HandleBallCollision(Collider other)
    {
        BallController ballController = other.GetComponent<BallController>();
        if (ballController != null)
        {
            string ballColor = ballController.color;
            Material newMaterial = GetMaterialByColor(ballColor);
            ChangeColor(newMaterial, other);
        }
    }

    private void ChangeColor(Material newMaterial, Collider other)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Renderer component not found on the light object!");
            return;
        }

        // Material der Lichtplatte ändern
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

        // Particle Animation
        AudioSource.PlayClipAtPoint(clip, playerCamera.transform.position, 0.2f);
        particleSystem.Play();
    }
}
