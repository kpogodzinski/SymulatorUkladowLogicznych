using UnityEngine;

public class Bulb : Element
{
    private void Update()
    {
        pins[0].GetComponent<SpriteRenderer>().color = inputs[0] ? Color.green : Color.red;
        GetComponent<SpriteChanger>().SetSprite(inputs[0] ? 1 : 0);
    }
}
