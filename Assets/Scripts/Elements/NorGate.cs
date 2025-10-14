using System.Linq;

public class NorGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs.Any(a => a))
            return false;
        return true;
    }
}
