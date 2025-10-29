using System;
using UnityEngine;

public abstract class Bistable : Element
{
    [SerializeField]
    protected bool clockPresent;
    protected bool previousClockSignal;
    protected bool currentClockSignal;

    protected abstract bool Evaluate();

    protected void Update()
    {
        if (clockPresent)
        {
            previousClockSignal = currentClockSignal;
            currentClockSignal = inputs[^1];
        }

        try
        {
            outputs[0] = Evaluate();
            outputs[1] = !outputs[0];
        }
        catch (ArgumentException)
        {
            outputs[0] = outputs[1] = false;
        }

        for (int i = 0; i < inputs.Count; i++)
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;

        for (int i = 0; i < outputs.Count; i++)
            pins[i + inputs.Count].GetComponent<SpriteRenderer>().color = outputs[i] ? Color.green : Color.red;
    }
}
