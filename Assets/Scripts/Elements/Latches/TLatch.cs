public class TLatch : Bistable
{
    protected override bool Evaluate()
    {
        if (inputs[1] && inputs[0])
            return !outputs[0];
        return outputs[0];
    }
}
