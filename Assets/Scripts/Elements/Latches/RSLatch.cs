using System;

public class RSLatch : Bistable
{
    protected override bool Evaluate()
    {
        switch ((inputs[0], inputs[1]))
        {
            case (false, false):
                return outputs[0];

            case (false, true):
                return true;

            case (true, false):
                return false;

            case (true, true):
                throw new ArgumentException();
        }
    }
}