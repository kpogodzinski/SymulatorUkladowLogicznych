using UnityEngine;

public class Wiring : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;

    private GameObject source;
    private GameObject target;

    [SerializeField]
    private Wire prefab;
    private Wire wire;
    private LineRenderer lr;

    private void OnTouchBegan(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            source = hit.collider.gameObject;
            if (source.tag.Contains("Pin"))
            {
                Vector2 touchPosition = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                wire = Instantiate(prefab, workspace.transform);
                wire.transform.localPosition = -workspace.transform.position / workspace.transform.localScale.x;

                lr = wire.GetComponent<LineRenderer>();
                lr.startColor = Color.black;
                lr.endColor = Color.black;
                lr.startWidth = (source.transform.localScale.x / 5) * wire.transform.parent.localScale.x;
                lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                lr.SetPosition(0, source.transform.position / workspace.transform.localScale.x);
                lr.SetPosition(1, touchPosition / workspace.transform.localScale.x);

                if (source.CompareTag("OutputPin"))
                {
                    wire.SetSource(source.transform.parent.gameObject);
                    wire.SetSourceIndex(source.transform.GetSiblingIndex() - source.GetComponentInParent<Element>().GetInputCount());
                }
                else if (source.CompareTag("InputPin"))
                {
                    wire.SetSource(source.transform.parent.gameObject);
                    wire.SetSourceIndex(source.transform.GetSiblingIndex());
                }
                else
                {
                    lr.startWidth *= 2f;
                    wire.SetSource(source);
                    wire.SetSourceIndex(-1);
                }
            }
        }
    }

    private void OnTouchMoved(Touch touch)
    {
        if (lr == null)
            return;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        lr.SetPosition(1, touchPosition / workspace.transform.localScale.x);
    }

    private void OnTouchEnded(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && lr != null)
        {
            target = hit.collider.gameObject;
            if (target.tag.Contains("Pin"))
            {
                lr.SetPosition(1, target.transform.position / workspace.transform.localScale.x);

                if (target.CompareTag("InputPin") && !source.CompareTag("InputPin"))
                {
                    wire.SetTarget(target.transform.parent.gameObject);
                    wire.SetTargetIndex(target.transform.GetSiblingIndex());
                }
                else if (target.CompareTag("OutputPin") && !source.CompareTag("OutputPin"))
                {
                    wire.SetTarget(target.transform.parent.gameObject);
                    wire.SetTargetIndex(target.transform.GetSiblingIndex() - target.GetComponentInParent<Element>().GetInputCount());
                }
                else if (target.CompareTag("ExternalPin"))
                {
                    wire.SetTarget(target);
                    wire.SetTargetIndex(-1);
                }
                else
                {
                    if (wire != null)
                        Destroy(wire.gameObject);
                }

                wire.SetConnected(true);
                if (source.CompareTag("InputPin") || target.CompareTag("OutputPin"))
                {
                    (target, source) = (source, target);
                    wire.SetSource(source.CompareTag("ExternalPin") ? source : source.transform.parent.gameObject);
                    wire.SetTarget(target.CompareTag("ExternalPin") ? target : target.transform.parent.gameObject);

                    int temp = wire.GetSourceIndex();
                    wire.SetSourceIndex(wire.GetTargetIndex());
                    wire.SetTargetIndex(temp);
                }
            }
            else
            {
                if (wire != null)
                    Destroy(wire.gameObject);
            }
        }
        else
        {
            if (wire != null)
                Destroy(wire.gameObject);
        }

        wire = null;
        lr = null;
        source = null;
        target = null;
    }

    private void Update()
    {
        if (!PlayerPrefs.GetString("TouchMode").Equals("Wiring"))
            return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                OnTouchBegan(touch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                OnTouchMoved(touch);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                OnTouchEnded(touch);
            }
        }
    }
}
