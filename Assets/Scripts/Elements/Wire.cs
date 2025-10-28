using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private Element source;
    private Element target;
    private int sourceOutputIndex;
    private int targetInputIndex;
    private bool connected;

    private LineRenderer lr;
    private PolygonCollider2D pc;

    public void SetSource(Element source)
    {
        this.source = source;
    }

    public void SetTarget(Element target)
    {
        this.target = target;
    }

    public void SetSourceIndex(int index)
    {
        sourceOutputIndex = index;
    }

    public void SetTargetIndex(int index)
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
        pc = GetComponent<PolygonCollider2D>();
        connected = false;
    }

    private void Update()
    {
        if (source == null || target == null)
        {
            if (connected)
                Destroy(gameObject);
            return;
        }

        bool signal = source.GetOutput(sourceOutputIndex);
        target.SetInput(targetInputIndex, signal);

        int childCount = source.transform.childCount;
        lr.SetPosition(0, source.transform.GetChild(childCount - 1).position);
        lr.SetPosition(1, target.transform.GetChild(targetInputIndex).position);
        lr.startWidth = (source.transform.localScale.x / 5) * transform.parent.localScale.x;

        Vector2 direction = (lr.GetPosition(1) - lr.GetPosition(0)).normalized;
        Vector2 perpDirection = Vector2.Perpendicular(direction);
        float lineWidth = lr.startWidth;
        pc.SetPath(0, new List<Vector2>()
        {
            (Vector2)lr.GetPosition(0) + lineWidth * perpDirection,
            (Vector2)lr.GetPosition(1) + lineWidth * perpDirection,
            (Vector2)lr.GetPosition(1) - lineWidth * perpDirection,
            (Vector2)lr.GetPosition(0) - lineWidth * perpDirection,
            (Vector2)lr.GetPosition(0) + lineWidth * perpDirection

        });
        pc.offset = -1 * lr.transform.position;
        lr.startColor = lr.endColor = source.transform.GetChild(childCount-1).GetComponent<SpriteRenderer>().color;
    }
    private void OnDestroy()
    {
        if (target != null)
            target.SetInput(targetInputIndex, false);
    }
}
