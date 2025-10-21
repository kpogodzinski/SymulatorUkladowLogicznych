using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;
    [SerializeField]
    private GameObject prefab;

    public void Spawn(Vector2 position)
    {
        Instantiate(prefab, position, Quaternion.identity, workspace.transform);
    }
}
