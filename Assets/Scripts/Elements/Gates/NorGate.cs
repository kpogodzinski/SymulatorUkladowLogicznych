using System.Linq;

public class NorGate : LogicGate
{
    public override bool Evaluate()
    {
        return !inputs.Any(a => a);
    }
}