using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject andGatePrefab;

    public void Spawn()
    {
        Instantiate(andGatePrefab, Vector3.zero, Quaternion.identity);
    }
}
