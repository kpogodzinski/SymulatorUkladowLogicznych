using UnityEngine;

public class Wire : MonoBehaviour
{
    private LogicGate source;
    private LogicGate target;
    private int targetInputIndex;

    public void SetSource(LogicGate source)
    {
        this.source = source;
    }

    public void SetTarget(LogicGate target)
    {
        this.target = target;
    }

    public void SetIndex(int index)
    {
        this.targetInputIndex = index;
    }

    private void Update()
    {
        if (source == null || target == null)
            return;

        bool signal = source.GetOutput();
        target.SetInput(targetInputIndex, signal);
    }
}
