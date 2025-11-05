using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool signal;
    public GameObject wireIn;
    public GameObject wireOut;

    public void SetSignal(bool signal)
    {
        this.signal = signal;
    }

    public bool GetSignal()
    {
        return signal;
    }

    public void Swap()
    {
        (wireIn, wireOut) = (wireOut, wireIn);
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = signal ? Color.green : Color.red;
    }
}
