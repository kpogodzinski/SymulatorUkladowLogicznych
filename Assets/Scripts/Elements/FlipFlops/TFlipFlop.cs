using System;

public class TFlipFlop : Bistable
{
    protected override bool Evaluate()
    {
        if (inputs[^3] && inputs[^2])
            throw new ArgumentException();

        if (inputs[^3])
            return true;

        if (inputs[^2])
            return false;

        if (!previousClockSignal && currentClockSignal)
            if (inputs[0])
                return !outputs[0];

        return outputs[0];
    }
}
