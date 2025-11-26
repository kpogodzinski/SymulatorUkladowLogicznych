using System.Linq;

public class AndGate : LogicGate
{
    public override bool Evaluate()
    {
        return inputs.All(a => a);
    }
}