using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField]
    protected int inputCount;
    [SerializeField]
    protected int outputCount;

    protected List<bool> inputs;
    protected List<bool> outputs;
    protected List<GameObject> pins;

    public void SetInput(int index, bool signal)
    {
        inputs[index] = signal;
    }

    public bool GetInput(int index)
    {
        return inputs[index];
    }

    public int GetInputCount()
    {
        return inputCount;
    }

    public bool GetOutput(int index)
    {
        return outputs[index];
    }

    protected List<GameObject> GetChildren()
    {
        List<GameObject> list = new();
        for (int i = 0; i < transform.childCount; i++)
            list.Add(transform.GetChild(i).gameObject);
        return list;
    }

    protected void Awake()
    {
        if (inputCount > 0)
        {
            inputs = new(inputCount);
            for (int i = 0; i < inputCount; i++)
                inputs.Add(false);
        }
        else
        {
            inputs = null;
        }

        if (outputCount > 0)
        {
            outputs = new(outputCount);
            for (int i = 0; i < outputCount; i++)
                outputs.Add(false);
        }
        else
        {
            outputs = null;
        }

        pins = GetChildren();
    }
}
