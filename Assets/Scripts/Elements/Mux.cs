using UnityEngine;

public class Mux : Element
{
    private int address;

    private void Update()
    {
        address = 0;
        if (inputs[^3])
            address += 4;
        if (inputs[^2])
            address += 2;
        if (inputs[^1])
            address += 1;

        outputs[0] = inputs[address];

        for (int i = 0; i < inputCount; i++)
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;
        pins[^1].GetComponent<SpriteRenderer>().color = outputs[0] ? Color.green : Color.red;
    }
}
