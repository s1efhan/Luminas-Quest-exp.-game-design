using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonbonCollector : MonoBehaviour
{
    public ScoreManager scoreManager;
    public GameObject bonbonPrefab;
    public Camera mainCamera;
    public AudioClip audioClip;

    void SpawnNewBonbon()
    {
        // Zufällige Position innerhalb des Umkreises von 25 Einheiten
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
        // Überprüfen, ob die zufällige Position auf dem Boden liegt
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity))
        {
            // Bonbon-Position mit Offset über dem Boden
            randomPosition = hit.point + Vector3.up * 1;
        }
        // Neues Bonbon an der zufälligen Position spawnen
        GameObject newObject = Instantiate(bonbonPrefab, randomPosition, Quaternion.identity);
        newObject.tag= "Bonbon";
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
           
        }
    }
}
