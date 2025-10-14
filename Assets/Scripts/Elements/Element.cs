using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    protected List<bool> inputs;
    protected bool output;
    protected short inputCount;
    protected List<GameObject> pins;

    public void SetInput(int index, bool signal)
    {
        inputs[index] = signal;
    }

    public bool GetInput(int index)
    {
        return inputs[index];
    }

    public bool GetOutput()
    {
        return output;
    }
}
