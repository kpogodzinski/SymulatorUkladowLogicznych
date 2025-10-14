using System.Linq;

public class XorGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs[0] == inputs[1])
            return false;
        return true;
    }
}
