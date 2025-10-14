using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private GameObject touchedObject;
    private Vector2 offset;
    private bool moving;

    private void Awake()
    {
        touchedObject = null;
        moving = false;
    }

    private void OnTouchBegan(Touch touch)
    {
        if (moving)
            return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Element"))
        {
            touchedObject = hit.collider.gameObject;
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 objectPosition = touchedObject.transform.position;
            offset = objectPosition - touchPosition;
            moving = true;
        }
    }

    private void OnTouchMoved(Touch touch)
    {
        if (!moving || touchedObject == null)
            return;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        touchedObject.transform.position = touchPosition + offset;
    }

    private void OnTouchEnded()
    {
        moving = false;
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
