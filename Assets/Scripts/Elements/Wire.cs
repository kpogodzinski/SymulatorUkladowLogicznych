using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private GameObject source;
    private GameObject target;
    private int sourceOutputIndex;
    private int targetInputIndex;
    private bool connected;

    private LineRenderer lr;
    private PolygonCollider2D pc;

    public void SetSource(GameObject source)
    {
        this.source = source;
    }

    public GameObject GetSource()
    {
        return source;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetSourceIndex(int index)
    {
        sourceOutputIndex = index;
    }

    public void SetTargetIndex(int index)
    {
        targetInputIndex = index;
    }

    public int GetSourceIndex()
    {
        return sourceOutputIndex;
    }

    public int GetTargetIndex()
    {
        return targetInputIndex;
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

        bool signal;

        if (sourceOutputIndex < 0) // if source is an external pin
        {
            signal = source.GetComponent<Pin>().GetSignal();
            lr.SetPosition(0, source.transform.position);
            lr.startColor = lr.endColor = source.GetComponent<SpriteRenderer>().color;
        }
        else
        {
            signal = source.GetComponent<Element>().GetOutput(sourceOutputIndex);
            lr.SetPosition(0, source.transform.GetChild(source.GetComponent<Element>().GetInputCount() + sourceOutputIndex).position);
            lr.startColor = lr.endColor = source.transform.GetChild(source.GetComponent<Element>().GetInputCount() + sourceOutputIndex).GetComponent<SpriteRenderer>().color;
        }

        if (targetInputIndex < 0) // if target is an external pin
        {
            target.GetComponent<Pin>().SetSignal(signal);
            lr.SetPosition(1, target.transform.position);
        }
        else
        {
            target.GetComponent<Element>().SetInput(targetInputIndex, signal);
            lr.SetPosition(1, target.transform.GetChild(targetInputIndex).position);
        }

        //target.GetComponent<Element>().SetInput(targetInputIndex, signal);

        //lr.SetPosition(0, source.transform.GetChild(source.GetComponent<Element>().GetInputCount() + sourceOutputIndex).position);
        //lr.SetPosition(1, target.transform.GetChild(targetInputIndex).position);

        lr.startWidth = (source.transform.localScale.x / 5) * transform.parent.localScale.x;

        ////////////////// COLLIDER SETTINGS //////////////////
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
        ///////////////////////////////////////////////////////

        //lr.startColor = lr.endColor = source.transform.GetChild(source.GetComponent<Element>().GetInputCount() + sourceOutputIndex).GetComponent<SpriteRenderer>().color;
    }
    private void OnDestroy()
    {
        if (target != null)
        {
            if (targetInputIndex < 0) // if target is an external pin
                Destroy(target);
            else
                target.GetComponent<Element>().SetInput(targetInputIndex, false);
        }
    }
}
