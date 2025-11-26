using System.Linq;

public class OrGate : LogicGate
{
    public override bool Evaluate()
    {
        return inputs.Any(a => a);
    }
}