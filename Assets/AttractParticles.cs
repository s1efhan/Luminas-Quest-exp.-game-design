using UnityEngine;

public class AttractParticles : MonoBehaviour
{
    public GameObject target; // Assign the player copy prefab here
    public ParticleSystem particles;
    public ParticleSystemForceField forceField;

    private void Start()
    {
        if (target != null && particles != null && forceField != null)
        {
            forceField.gameObject.SetActive(true);
            forceField.transform.position = target.transform.position; // Start at target position
        }
    }

    private void Update()
    {
        if (target != null)
            forceField.transform.position = target.transform.position; // Continuously update position to follow the target
    }
}
