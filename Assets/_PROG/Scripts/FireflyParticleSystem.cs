using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireflyMeshEmitter : MonoBehaviour
{
    public Mesh shapeMesh; // Das Mesh, das als Form für das Partikelsystem dient

    void Start()
    {
        ParticleSystem fireflySystem = GetComponent<ParticleSystem>();

        var main = fireflySystem.main;
        main.startSpeed = 0.1f;  // Sehr niedrige Startgeschwindigkeit
        main.startSize = 0.1f;
        main.startColor = new Color(1.0f, 0.8f, 0.4f);

        var shape = fireflySystem.shape;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = shapeMesh;

        var velocity = fireflySystem.velocityOverLifetime;
        velocity.x = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);
        velocity.y = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);
        velocity.z = new ParticleSystem.MinMaxCurve(-0.5f, 0.5f);

        var colorOverLifetime = fireflySystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) },
                     new GradientAlphaKey[] { new GradientAlphaKey(0.5f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.5f, 1.0f) });
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(grad);
    }
}

