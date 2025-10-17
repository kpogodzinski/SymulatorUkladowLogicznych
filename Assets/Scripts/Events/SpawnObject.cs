using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;
    [SerializeField]
    private GameObject prefab;

    public void Spawn()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity, workspace.transform);
    }
}
