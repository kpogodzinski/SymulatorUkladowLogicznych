using UnityEngine;
using UnityEngine.EventSystems;

public class WorkspaceScaling : MonoBehaviour
{
    private bool beganOverUI;
    private float initialDistance;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Vector3 touchPivot;

    private void OnTouchBegan(Touch touch0, Touch touch1)
    {
        initialDistance = (touch1.position - touch0.position).magnitude;
        initialPosition = transform.position;
        initialScale = transform.localScale;

        touchPivot = Vector2.Lerp(
            Camera.main.ScreenToWorldPoint(touch0.position),
            Camera.main.ScreenToWorldPoint(touch1.position),
            0.5f);
    }

    private void OnTouchMoved(Touch touch0, Touch touch1)
    {
        float newDistance = (touch1.position - touch0.position).magnitude;
        float distanceRatio = newDistance / initialDistance;

        Vector3 newScale = initialScale * distanceRatio;
        Vector3 newPosition = touchPivot + (initialPosition - touchPivot) * (newScale.x / initialScale.x);
        transform.localScale = newScale;
        transform.position = newPosition;
    }

    private void OnTouchEnded()
    {

    }

    private void Update()
    {
        if (/*PlayerPrefs.GetString("TouchMode").Equals("Camera") && */Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (/*touch0.phase == TouchPhase.Began && */touch1.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch0.fingerId) || EventSystem.current.IsPointerOverGameObject(touch1.fingerId))
                {
                    beganOverUI = true;
                    return;
                }
                else
                {
                    OnTouchBegan(touch0, touch1);
                }
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if (!beganOverUI)
                {
                    OnTouchMoved(touch0, touch1);
                }
            }
            else if (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled
                || touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled)
            {
                OnTouchEnded();
            }
        }
    }
}
