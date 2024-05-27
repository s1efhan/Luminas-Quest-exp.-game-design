using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombinedMesh : MonoBehaviour
{
    void Start()
    {
        // Erstellen der Komponenten
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Skalierung der Objekte
        cylinder.transform.localScale = new Vector3(1, 4, 1);
        sphere.transform.localScale = new Vector3(8, 8, 8);

        // Positionierung der Kugel auf dem Zylinder
        sphere.transform.position = new Vector3(0, 5.25f, 0);

        // Kombiniere die Meshes
        CombineMeshes(cylinder, sphere);

        // Lösche die temporären Objekte
        Destroy(cylinder);
        Destroy(sphere);
    }

    void CombineMeshes(GameObject cylinder, GameObject sphere)
    {
        MeshFilter cylinderMeshFilter = cylinder.GetComponent<MeshFilter>();
        MeshFilter sphereMeshFilter = sphere.GetComponent<MeshFilter>();

        CombineInstance[] combine = new CombineInstance[2];
        combine[0].mesh = cylinderMeshFilter.sharedMesh;
        combine[0].transform = cylinder.transform.localToWorldMatrix;
        combine[1].mesh = sphereMeshFilter.sharedMesh;
        combine[1].transform = sphere.transform.localToWorldMatrix;

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = combinedMesh;
    }
}
