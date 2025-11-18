using System.Linq;
using UnityEngine;

public class RGBLed : Element
{
    private void Update()
    {
        GetComponent<SpriteChanger>().SetSprite(inputs.Any(input => input) ? 1 : 0);

        Color color = Color.black;
        if (inputs[0])
            color += Color.red;
        if (inputs[1])
            color += Color.green;
        if (inputs[2])
            color += Color.blue;

        GetComponent<SpriteRenderer>().color = color;

        for (int i = 0; i < inputCount; i++)
        {
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;
        }
    }
}
