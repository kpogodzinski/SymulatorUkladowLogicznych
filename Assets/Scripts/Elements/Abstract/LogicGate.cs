using UnityEngine;

public abstract class LogicGate : Element
{
    public abstract bool Evaluate();

    protected void LateUpdate()
    {
        outputs[0] = Evaluate();

        for (int i = 0; i < pins.Count - 1; i++)
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;

        pins[^1].GetComponent<SpriteRenderer>().color = outputs[0] ? Color.green : Color.red;
    }
}
