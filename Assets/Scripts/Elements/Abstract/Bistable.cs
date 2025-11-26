using System;
using UnityEngine;

public abstract class Bistable : Element
{
    [SerializeField]
    protected bool clockPresent;
    protected bool previousClockSignal;
    protected bool currentClockSignal;

    protected abstract bool Evaluate();

    protected override void Update()
    {
        base.Update();

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
    }
}