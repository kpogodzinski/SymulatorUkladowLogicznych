using UnityEngine;

public class Wiring : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;
    [SerializeField]
    private GameObject popup;

    private GameObject source;
    private GameObject target;

    [SerializeField]
    private Wire prefab;
    private Wire wire;
    private LineRenderer lr;

    private void WiringErrorPopup()
    {
        popup.GetComponent<Popup>().ShowPopup("Cannot connect a wire to an already connected external pin!", 1.5f);
    }

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
                lr.startWidth = (source.transform.localScale.x / 5) * workspace.transform.localScale.x;
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
        if (source != null && source.CompareTag("ExternalPin") && source.GetComponent<Pin>().IsConnected())
        {
            WiringErrorPopup();

            if (wire != null)
            {
                Destroy(wire.gameObject);
                wire = null;
                lr = null;
                source = null;
                target = null;
                return;
            }
        }

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
                else if (target.CompareTag("ExternalPin") && !target.GetComponent<Pin>().IsConnected())
                {
                    wire.SetTarget(target);
                    wire.SetTargetIndex(-1);
                }
                else
                {
                    WiringErrorPopup();

                    if (wire != null)
                    {
                        Destroy(wire.gameObject);
                        return;
                    }
                }

                wire.SetConnected(true);
                if (source.CompareTag("InputPin") || target.CompareTag("OutputPin"))
                {
                    (source, target) = (target, source);
                    wire.Swap();
                }

                if (source.CompareTag("ExternalPin"))
                {
                    if (source.GetComponent<Pin>().GetWireOut() != null)
                        source.GetComponent<Pin>().Swap();
                    source.GetComponent<Pin>().SetWireOut(wire.gameObject);
                }
                if (target.CompareTag("ExternalPin"))  
                {
                    if (target.GetComponent<Pin>().GetWireIn() != null)
                        target.GetComponent<Pin>().Swap();
                    target.GetComponent<Pin>().SetWireIn(wire.gameObject);
                }

                if (source.CompareTag("OutputPin") && target.CompareTag("ExternalPin"))
                {
                    var tempTarget = target;
                    var tempWire = tempTarget.GetComponent<Pin>().GetWireOut();
                    while (tempWire != null)
                    {
                        if (tempWire.GetComponent<Wire>().GetSource() != tempTarget)
                        {
                            tempWire.GetComponent<Wire>().Swap();
                            if (tempWire.GetComponent<Wire>().GetTarget().CompareTag("ExternalPin"))
                            {
                                tempWire.GetComponent<Wire>().GetTarget().GetComponent<Pin>().Swap();
                            }
                        }

                        tempTarget = tempWire.GetComponent<Wire>().GetTarget();
                        if (tempTarget.CompareTag("ExternalPin"))
                        {
                            if (tempWire == tempTarget.GetComponent<Pin>().GetWireOut())
                            {
                                tempTarget.GetComponent<Pin>().Swap();
                                tempWire = null;
                            }
                            else 
                            {
                                tempWire = tempTarget.GetComponent<Pin>().GetWireOut();
                            }
                        }
                        else
                        {
                            tempWire = null;
                        }
                    }
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
