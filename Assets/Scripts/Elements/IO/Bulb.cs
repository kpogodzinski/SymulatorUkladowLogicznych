using UnityEngine;

public class Bulb : Element
{
    protected override void LateUpdate()
    {
        pins[0].GetComponent<SpriteRenderer>().color = inputs[0] ? Color.green : Color.red;
        GetComponent<SpriteChanger>().SetSprite(inputs[0] ? 1 : 0);
    }
}