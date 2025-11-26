using System;

public class JKFlipFlop : JKLatch
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
            return base.Evaluate();

        return outputs[0];
    }
}