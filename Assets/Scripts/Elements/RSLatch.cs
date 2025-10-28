using System;

public class RSLatch : Latch
{
    protected override bool Evaluate()
    {
        if ((inputs[0], inputs[1]) == (false, false))
            return outputs[0];

        if ((inputs[0], inputs[1]) == (false, true))
            return true;

        if ((inputs[0], inputs[1]) == (true, false))
            return false;

        if ((inputs[0], inputs[1]) == (true, true))
            throw new ArgumentException();

        return false;

    }
}
