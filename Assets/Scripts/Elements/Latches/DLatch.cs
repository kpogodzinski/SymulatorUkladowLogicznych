public class DLatch : Latch
{
    protected override bool Evaluate()
    {
        if (inputs[1])
            return inputs[0];
        return outputs[0];
    }
}
