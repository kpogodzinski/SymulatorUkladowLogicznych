public class DLatch : Bistable
{
    protected override bool Evaluate()
    {
        if (inputs[1])
            return inputs[0];
        return outputs[0];
    }
}
