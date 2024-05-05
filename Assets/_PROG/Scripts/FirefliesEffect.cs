using UnityEngine;

public class FirefliesEffect : MonoBehaviour
{
    public ParticleSystem firefliesParticles;
    private Transform target;
    public float speed = 10f;

    private ParticleSystem.Particle[] particles;
    private bool isActive = false; // Only activate particle movement when target is set

    void Start()
    {
        particles = new ParticleSystem.Particle[firefliesParticles.main.maxParticles];
        firefliesParticles.Play(); // Start the particle system in an idle state
    }

    void Update()
    {
        if (!isActive || target == null)
            return;

        int numParticlesAlive = firefliesParticles.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 particleToTarget = target.position - particles[i].position;
            float distance = particleToTarget.magnitude;
            Vector3 velocity = (distance > 1f) ? particleToTarget.normalized * speed : Vector3.zero;
            particles[i].velocity = velocity;

            // Optionally add code to dissipate particles as they reach the target
            if (distance < 1f)
                particles[i].remainingLifetime = 0;
        }

        firefliesParticles.SetParticles(particles, numParticlesAlive);
    }

    public void ActivateParticles(Transform newTarget)
    {
        target = newTarget;
        isActive = true;
    }

    public void DeactivateParticles()
    {
        isActive = false;
        firefliesParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
