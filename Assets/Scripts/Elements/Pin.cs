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
}
