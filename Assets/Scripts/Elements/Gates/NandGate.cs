using System.Linq;

public class NandGate : LogicGate
{
    public override bool Evaluate()
    {
        return !inputs.All(a => a);
    }
}