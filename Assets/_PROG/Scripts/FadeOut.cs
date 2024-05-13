using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Material material;
    private Color originalColor;
    private Color originalEmissionColor;
    private float startTime;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
        originalEmissionColor = material.GetColor("_EmissionColor");
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / fadeDuration;
        Color color = originalColor;
        color.a = Mathf.Lerp(originalColor.a, 0, t);
        material.color = color;

        Color emissionColor = originalEmissionColor;
        emissionColor *= Mathf.Lerp(1.0f, 0f, t); // Reduziere die Emissionsintensität linear
        material.SetColor("_EmissionColor", emissionColor);

        if (color.a <= 0.1f) Destroy(gameObject);
    }
}
