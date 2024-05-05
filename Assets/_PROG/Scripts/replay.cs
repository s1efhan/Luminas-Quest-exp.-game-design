using System.Collections.Generic;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    public GameObject playerPrefab;
    public float recordTime = 30f;
    public FirefliesEffect firefliesEffect; // Reference to FirefliesEffect
    private Queue<(Vector3, Quaternion, float)> movements = new Queue<(Vector3, Quaternion, float)>();
    private GameObject replayInstance;
    private float timer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && replayInstance == null)
        {
            StartReplay();
        }

        RecordPlayerMovement();
    }

    private void RecordPlayerMovement()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / 10f) // Record 10 times per second
        {
            if (movements.Count >= recordTime * 10)
            {
                movements.Dequeue();
            }
            movements.Enqueue((transform.position, transform.rotation, Time.time));
            timer = 0f;
        }
    }

    private System.Collections.IEnumerator ReplayMovement()
    {
        var movementsArray = movements.ToArray(); // Convert the queue to an array for easier access
        float startTime = movementsArray[0].Item3;

        for (int i = 0; i < movementsArray.Length - 1; i++)
        {
            var (startPosition, startRotation, _) = movementsArray[i];
            var (endPosition, endRotation, endTime) = movementsArray[i + 1];

            float duration = endTime - movementsArray[i].Item3;
            float elapsed = 0;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                replayInstance.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                replayInstance.transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        Destroy(replayInstance);
        firefliesEffect.DeactivateParticles(); // Stop fireflies effect
        replayInstance = null;
    }

    private void StartReplay()
    {
        if (movements.Count > 0)
        {
            var (startPosition, startRotation, _) = movements.Peek();
            replayInstance = Instantiate(playerPrefab, startPosition, startRotation);

            // Activate the fireflies effect
            firefliesEffect.ActivateParticles(replayInstance.transform);

            StartCoroutine(ReplayMovement());
        }
    }
}
