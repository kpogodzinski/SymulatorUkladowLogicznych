using UnityEngine;

public abstract class LogicGate : Element
{
    public abstract bool Evaluate();

    protected override void Update()
    {
        base.Update();

        outputs[0] = Evaluate();
    }
}