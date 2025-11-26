public class NotGate : LogicGate
{
    public override bool Evaluate()
    {
        return !inputs[0];
    }
}