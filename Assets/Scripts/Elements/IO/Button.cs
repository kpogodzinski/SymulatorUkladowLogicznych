using UnityEngine;

public class Button : Element
{
    public void SetOutput(bool value)
    {
        outputs[0] = value;
    }

    private void LateUpdate()
    {
        pins[0].GetComponent<SpriteRenderer>().color = outputs[0] ? Color.green : Color.red;
    }
}
