using UnityEngine;

public class Wire : MonoBehaviour
{
    private LogicGate source;
    private LogicGate target;
    private int targetInputIndex;

    private LineRenderer lr;

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
        targetInputIndex = index;
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (source == null || target == null)
            return;

        bool signal = source.GetOutput();
        target.SetInput(targetInputIndex, signal);

        int childCount = source.transform.childCount;
        lr.SetPosition(0, source.transform.GetChild(childCount - 1).position);
        lr.SetPosition(1, target.transform.GetChild(targetInputIndex).position);
    }
}
