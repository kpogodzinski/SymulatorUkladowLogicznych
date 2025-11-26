public class XorGate : LogicGate
{
    public override bool Evaluate()
    {
        return inputs[0] != inputs[1];
    }
}