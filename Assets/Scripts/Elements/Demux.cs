using UnityEngine;

public class Demux : Element
{
    private int address;

    private void Update()
    {
        outputs[address] = false;

        address = 0;
        if (inputs[^3])
            address += 4;
        if (inputs[^2])
            address += 2;
        if (inputs[^1])
            address += 1;

        outputs[address] = inputs[0];

        for (int i = 0; i < inputCount; i++)
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;

        for (int i = 0; i <  outputCount; i++)
            pins[i+inputCount].GetComponent<SpriteRenderer>().color = outputs[i] ? Color.green : Color.red;
    }
}
