using System.Linq;

public class OrGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs.Any(a => a))
            return true;
        return false;
    }
}
