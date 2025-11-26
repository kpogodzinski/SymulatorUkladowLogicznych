using UnityEngine;

public class Button : Element
{
    public void SetOutput(bool value)
    {
        outputs[0] = value;
    }
}