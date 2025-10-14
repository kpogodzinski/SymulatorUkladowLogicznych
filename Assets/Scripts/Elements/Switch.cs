using UnityEngine;

public class Switch : Element
{
    public void SetOutput(bool value)
    {
        output = value;
    }

    private void Update()
    {
        pins[0].GetComponent<SpriteRenderer>().color = output ? Color.green : Color.red;
    }
}
