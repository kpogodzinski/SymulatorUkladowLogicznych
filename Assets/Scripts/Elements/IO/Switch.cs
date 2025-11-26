using UnityEngine;

public class Switch : Element
{
    public void SetOutput(bool value)
    {
        outputs[0] = value;
    }
}