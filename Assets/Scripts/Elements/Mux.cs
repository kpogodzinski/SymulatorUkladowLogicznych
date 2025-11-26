using UnityEngine;

public class Mux : Element
{
    private int address;

    protected override void Update()
    {
        base.Update();

        address = 0;
        if (inputs[^3])
            address += 4;
        if (inputs[^2])
            address += 2;
        if (inputs[^1])
            address += 1;

        outputs[0] = inputs[address];
    }
}