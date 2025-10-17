using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;
    [SerializeField]
    private GameObject scrollView;
    private ScrollRect scrollRect;

    private GameObject touchedObject;
    private Vector2 offset;
    private bool objectMoving;

    private void Awake()
    {
        touchedObject = null;
        objectMoving = false;
        scrollRect = scrollView.GetComponent<ScrollRect>();
    }

    public bool IsMoving()
    {
        return objectMoving;
    }

    private void OnTouchBegan(Touch touch)
    {
        if (objectMoving)
            return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            objectMoving = true;

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (hit.collider.gameObject.CompareTag("Element"))
            {
                if (!PlayerPrefs.GetString("TouchMode").Equals("Moving"))
                    return;

                touchedObject = hit.collider.gameObject;
                Vector2 objectPosition = touchedObject.transform.position;
                offset = objectPosition - touchPosition;
            }
            else if (hit.collider.gameObject.CompareTag("NewElement"))
            {
                scrollRect.enabled = false;
                touchedObject = Instantiate(hit.collider.gameObject, touchPosition, Quaternion.identity, workspace.transform);
                touchedObject.transform.localScale = 0.5f * 1.2f * Vector3.one;
                touchedObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
                offset = Vector3.zero;
            }
        }
    }

    private void OnTouchMoved(Touch touch)
    {
        if (!objectMoving || touchedObject == null)
            return;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchedObject.transform.position = touchPosition + offset;
    }

    private void OnTouchEnded()
    {
        if (touchedObject != null && touchedObject.CompareTag("NewElement"))
        {
            touchedObject.transform.localScale /= 1.2f;
            touchedObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
            touchedObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            foreach (SpriteRenderer child in touchedObject.transform.GetComponentsInChildren<SpriteRenderer>())
                child.sortingLayerName = "Default";
            touchedObject.tag = "Element";

            if (touchedObject.GetComponent<LogicGate>() != null)
                touchedObject.GetComponent<LogicGate>().enabled = true;
            if (touchedObject.GetComponent<Switch>() != null)
                touchedObject.GetComponent<Switch>().enabled = true;
            if (touchedObject.GetComponent<ToggleSwitch>() != null)
                touchedObject.GetComponent <ToggleSwitch>().enabled = true;
            if (touchedObject.GetComponent<Bulb>() != null)
                touchedObject.GetComponent<Bulb>().enabled = true;

            scrollRect.enabled = true;
        }
        objectMoving = false;
        touchedObject = null;
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
                OnTouchEnded();
            }
        }
    }
}
