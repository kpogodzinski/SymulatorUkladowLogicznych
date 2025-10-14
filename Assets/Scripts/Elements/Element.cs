using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{
    [SerializeField]
    protected short inputCount;

    protected List<bool> inputs;
    protected bool output;
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

        pins = GetChildren();
    }
}
