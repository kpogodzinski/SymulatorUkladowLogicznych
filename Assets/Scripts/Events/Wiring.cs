using UnityEngine;

public class Wiring : MonoBehaviour
{
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
            if (/*source.CompareTag("InputPin") || */source.CompareTag("OutputPin") && source.transform.parent.CompareTag("Element"))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                wire = Instantiate(prefab);
                wire.transform.position = ray.origin;

                lr = wire.GetComponent<LineRenderer>();
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
        if (hit.collider != null && lr != null)
        {
            target = hit.collider.gameObject;
            if (target.CompareTag("InputPin") && target.transform.parent.CompareTag("Element")/* || target.CompareTag("OutputPin")*/)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                lr.SetPosition(1, target.transform.position);

                wire.SetSource(source.transform.parent.GetComponent<Element>());
                wire.SetTarget(target.transform.parent.GetComponent<Element>());

                int index = target.transform.GetSiblingIndex();
                wire.SetIndex(index);
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
