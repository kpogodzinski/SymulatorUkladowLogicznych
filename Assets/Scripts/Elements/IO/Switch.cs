using UnityEngine;

public class Switch : Element
{
    public void SetOutput(bool value)
    {
        outputs[0] = value;
    }

    private void Update()
    {
        pins[0].GetComponent<SpriteRenderer>().color = outputs[0] ? Color.green : Color.red;
    }
}
