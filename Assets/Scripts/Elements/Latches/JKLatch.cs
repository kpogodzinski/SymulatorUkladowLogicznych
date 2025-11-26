public class JKLatch : Bistable
{
    protected override bool Evaluate()
    {
        switch ((inputs[0], inputs[1]))
        {
            case (false, false):
                return outputs[0];

            case (true, false):
                return true;

            case (false, true):
                return false;

            case (true, true):
                return !outputs[0];
        }
    }
}