using UnityEngine;
using UnityEngine.EventSystems;

public class WorkspaceScaling : MonoBehaviour
{
    private bool beganOverUI;
    private float currentDistance;

    private void OnTouchBegan(Touch touch0, Touch touch1)
    {
        currentDistance = (touch1.position - touch0.position).magnitude;
    }

    private void OnTouchMoved(Touch touch0, Touch touch1)
    {
        float newDistance = (touch1.position - touch0.position).magnitude;
        float distanceRatio = newDistance / currentDistance;

        transform.localScale *= distanceRatio;
        currentDistance = newDistance;
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
