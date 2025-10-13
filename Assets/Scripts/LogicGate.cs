using System.Collections.Generic;
using UnityEngine;

public abstract class LogicGate : MonoBehaviour
{
    protected List<bool> inputs;
    protected bool output;
    protected short inputCount;

    protected List<GameObject> pins;

    public abstract bool Evaluate();

    protected List<GameObject> GetChildren()
    {
        List<GameObject> list = new();
        for (int i = 0; i < transform.childCount; i++)
            list.Add(transform.GetChild(i).gameObject);
        return list;
    }
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
