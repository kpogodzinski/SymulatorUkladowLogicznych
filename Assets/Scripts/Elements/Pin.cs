using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool signal;

    public void SetSignal(bool signal)
    {
        this.signal = signal;
    }

    public bool GetSignal()
    {
        return signal;
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = signal ? Color.green : Color.red;
    }

    private void OnDestroy()
    {
        Transform wire = transform.GetChild(0);
        wire.parent = transform.parent;
        wire.gameObject.SetActive(true);
        wire.GetComponent<Wire>().enabled = true;
        wire.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
