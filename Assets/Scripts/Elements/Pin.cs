using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool signal;
    private bool connected;
    private GameObject wireIn;
    private GameObject wireOut;

    public void SetSignal(bool signal)
    {
        this.signal = signal;
    }

    public bool GetSignal()
    {
        return signal;
    }

    public bool IsConnected()
    {
        return connected;
    }

    public GameObject GetWireIn()
    {
        return wireIn;
    }
    
    public GameObject GetWireOut()
    {
        return wireOut;
    }

    public void SetWireIn(GameObject wireIn)
    {
        this.wireIn = wireIn;
    }

    public void SetWireOut(GameObject wireOut)
    {
        this.wireOut = wireOut;
    }

    public void Swap()
    {
        (wireIn, wireOut) = (wireOut, wireIn);
    }

    private void LateUpdate()
    {
        connected = wireIn != null && wireOut != null;
        GetComponent<SpriteRenderer>().color = signal ? Color.green : Color.red;
    }
}
