using System.Collections;
using UnityEngine;

public class BonbonCollector : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject bonbonPrefab;
    public Camera mainCamera;
    public AudioClip audioClip;
    public GameObject firefliesParent; // Reference to the parent GameObject "Fireflies"

    void SpawnNewBonbon()
    {
        // Zuf�llige Position innerhalb des Umkreises von 25 Einheiten
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
        // �berpr�fen, ob die zuf�llige Position auf dem Boden liegt
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity))
        {
            // Bonbon-Position mit Offset �ber dem Boden
            randomPosition = hit.point + Vector3.up * 1;
        }
        // Neues Bonbon an der zuf�lligen Position spawnen
        GameObject newObject = Instantiate(bonbonPrefab, randomPosition, Quaternion.identity);

        // Set the parent of the new Bonbon object to "Fireflies"
        if (firefliesParent != null)
        {
            newObject.transform.parent = firefliesParent.transform;
        }

        newObject.tag = "Bonbon";
        if (newObject != null)
        {
            newObject.GetComponentInChildren<Light>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonbon"))
        {
            Debug.Log("Kollision mit Bonbon erkannt!");
            scoreManager.IncreaseCount();
            SpawnNewBonbon();
            Destroy(other.gameObject); // Optional: Die eingesammelte Kugel entfernen
            AudioSource.PlayClipAtPoint(audioClip, mainCamera.transform.position);
            GetComponentInChildren<Light>().intensity = GetComponentInChildren<Light>().intensity + 0.5f;
            GetComponentInChildren<Light>().range = GetComponentInChildren<Light>().range + 1f;
        }
    }
}
