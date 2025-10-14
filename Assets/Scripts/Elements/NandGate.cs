using System.Linq;

public class NandGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs.Any(a => !a))
            return true;
        return false;
    }
}
