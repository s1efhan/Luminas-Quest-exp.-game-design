using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRecorder : MonoBehaviour
{
    public List<Vector3> RecordedPositions { get; set; }
    public float ReplaySpeed = 1.0f;

    private int currentPositionIndex = 0;

    public void ReplayRecordedActions()
    {
        StartCoroutine(ReplayCoroutine());
    }

    IEnumerator ReplayCoroutine()
    {
        while (currentPositionIndex < RecordedPositions.Count - 1)
        {
            Vector3 currentPos = RecordedPositions[currentPositionIndex];
            Vector3 nextPos = RecordedPositions[currentPositionIndex + 1];
            transform.position = Vector3.Lerp(currentPos, nextPos, Time.deltaTime * ReplaySpeed);
            yield return null;
        }
    }
}
