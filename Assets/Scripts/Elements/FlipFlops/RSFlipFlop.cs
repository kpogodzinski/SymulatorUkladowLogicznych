public class RSFlipFlop : RSLatch
{
    protected override bool Evaluate()
    {
        if (!previousClockSignal && currentClockSignal)
            return base.Evaluate();
        else
            return outputs[0];
    }
}