using Unity.VisualScripting;
using UnityEngine;

public class Wiring : MonoBehaviour
{
    private GameObject source;
    private GameObject target;

    private LineRenderer lr;

    private void OnTouchBegan(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            source = hit.collider.gameObject;
            if (source.CompareTag("InputPin") || source.CompareTag("OutputPin"))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                GameObject dummy = new("dummy", typeof(LineRenderer));
                dummy.transform.position = ray.origin;

                lr = dummy.GetComponent<LineRenderer>();
                lr.startColor = Color.black;
                lr.endColor = Color.black;
                lr.startWidth = source.transform.localScale.x / 5;
                lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                lr.SetPosition(0, source.transform.position);
                lr.SetPosition(1, touchPosition);
            }
        }
    }

    private void OnTouchMoved(Touch touch)
    {
        if (lr == null)
            return;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        lr.SetPosition(1, touchPosition);
    }

    private void OnTouchEnded(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            if (target.CompareTag("InputPin") || target.CompareTag("OutputPin"))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                lr.SetPosition(1, target.transform.position);

                GameObject obj = new("Wire", typeof(Wire));
                Wire wire = obj.GetComponent<Wire>();
                wire.SetSource(source.transform.parent.GetComponent<LogicGate>());
                wire.SetTarget(target.transform.parent.GetComponent<LogicGate>());

                int index = target.transform.GetSiblingIndex();
                wire.SetIndex(index);

                wire.AddComponent<LineRenderer>();
                LineRenderer wlr = wire.GetComponent<LineRenderer>();
                wlr.material = lr.material;
                wlr.startColor = lr.startColor;
                wlr.endColor = lr.endColor;
                wlr.startWidth = lr.startWidth;
                wlr.SetPosition(0, lr.GetPosition(0));
                wlr.SetPosition(1, lr.GetPosition(1));
            }
        }
        
        Destroy(GameObject.Find("dummy"));
    }


    private void Update()
    {
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
