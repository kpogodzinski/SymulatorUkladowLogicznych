using System.Linq;

public class AndGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs.Any(a => !a))
            return false;
        return true;
    }
}
