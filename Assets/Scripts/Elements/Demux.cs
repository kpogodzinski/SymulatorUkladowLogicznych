using UnityEngine;

public class Demux : Element
{
    private int address;

    protected override void Update()
    {
        base.Update();

        outputs[address] = false;

        address = 0;
        if (inputs[^3])
            address += 4;
        if (inputs[^2])
            address += 2;
        if (inputs[^1])
            address += 1;

        outputs[address] = inputs[0];
    }
}