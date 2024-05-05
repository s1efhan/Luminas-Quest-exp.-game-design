using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public ParticleSystem fireflyParticles;
    public Transform fireflyBodyTarget;
    public bool activateFireflyBody = false;

    void Update()
    {
        if (activateFireflyBody)
        {
            fireflyParticles.Stop(); // Stoppt die Emission von neuen Partikeln

            var main = fireflyParticles.main;
            main.startSpeed = 10; // Erhöht die Geschwindigkeit, um die Partikel schnell zum Ziel zu bewegen

            var emission = fireflyParticles.emission;
            emission.rateOverTime = 0; // Stoppt die weitere Emission

            // Lenkt alle existierenden Partikel zum Zielort
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[fireflyParticles.particleCount];
            int numParticlesAlive = fireflyParticles.GetParticles(particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 directionToTarget = (fireflyBodyTarget.position - particles[i].position).normalized;
                particles[i].velocity = directionToTarget * 10; // Bewegt die Partikel zum Ziel
            }
            fireflyParticles.SetParticles(particles, numParticlesAlive);
        }
    }

    public void ActivateFireflyBody()
    {
        activateFireflyBody = true;
    }
}
