using UnityEngine;

public class CombineSkinnedMeshes : MonoBehaviour
{
    void Start()
    {
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        CombineInstance[] combine = new CombineInstance[skinnedMeshRenderers.Length];
        int i = 0;

        while (i < skinnedMeshRenderers.Length)
        {
            combine[i].mesh = skinnedMeshRenderers[i].sharedMesh;
            combine[i].transform = skinnedMeshRenderers[i].transform.localToWorldMatrix;
            skinnedMeshRenderers[i].gameObject.SetActive(false);
            i++;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine);

        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(transform);
        combinedObject.transform.localPosition = Vector3.zero;
        combinedObject.transform.localRotation = Quaternion.identity;
        combinedObject.transform.localScale = Vector3.one;

        SkinnedMeshRenderer smr = combinedObject.AddComponent<SkinnedMeshRenderer>();
        smr.sharedMesh = combinedMesh;
        smr.bones = skinnedMeshRenderers[0].bones;
        smr.rootBone = skinnedMeshRenderers[0].rootBone;
    }
}
