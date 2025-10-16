using UnityEngine;

public class Wire : MonoBehaviour
{
    private Element source;
    private Element target;
    private int targetInputIndex;
    private bool connected;

    private LineRenderer lr;

    public void SetSource(Element source)
    {
        this.source = source;
    }

    public void SetTarget(Element target)
    {
        this.target = target;
    }

    public void SetIndex(int index)
    {
        targetInputIndex = index;
    }

    public void SetConnected(bool connected)
    {
        this.connected = connected;
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        connected = false;
    }

    private void Update()
    {
        if (source == null || target == null)
        {
            if (connected)
            {
                if (target != null)
                    target.SetInput(targetInputIndex, false);
                Destroy(gameObject);
            }
            return;
        }

        bool signal = source.GetOutput();
        target.SetInput(targetInputIndex, signal);

        int childCount = source.transform.childCount;
        lr.SetPosition(0, source.transform.GetChild(childCount - 1).position);
        lr.SetPosition(1, target.transform.GetChild(targetInputIndex).position);
        lr.startColor = lr.endColor = source.transform.GetChild(childCount-1).GetComponent<SpriteRenderer>().color;
    }
}
