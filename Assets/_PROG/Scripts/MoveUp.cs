using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed = 50.0f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
