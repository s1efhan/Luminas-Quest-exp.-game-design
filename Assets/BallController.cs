using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public string color; // Die Farbe des Balls, die vom Spieler gesetzt wird
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material yellowMaterial;
    public Material defaultMaterial;

    private Dictionary<string, Material> materialMap = new Dictionary<string, Material>();

    void Awake()
    {
        // Materialien zu Farben hinzufügen
        materialMap.Add("red", redMaterial);
        materialMap.Add("blue", blueMaterial);
        materialMap.Add("green", greenMaterial);
        materialMap.Add("yellow", yellowMaterial);
    }

    void Start()
    {
        // Hier wird angenommen, dass der Ball die Farbe des Spielers bei der Erstellung erhält
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            color = playerController.currentColor;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = GetMaterialByColor(color);
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
}
