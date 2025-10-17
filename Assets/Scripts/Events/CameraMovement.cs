using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector2 previousTouch;
    private bool beganOverUI;

    private void Awake()
    {
        beganOverUI = false;
    }

    private void OnTouchBegan(Touch touch)
    {
        previousTouch = touch.position;
    }

    private void OnTouchMoved(Touch touch)
    {
        Vector3 direction = (touch.position - previousTouch).normalized;
        float distance = Vector3.Magnitude(
            Camera.main.ScreenToWorldPoint(touch.position) - Camera.main.ScreenToWorldPoint(previousTouch)
            );
        Camera.main.transform.position -= direction * distance;
        previousTouch = touch.position;
    }

    private void OnTouchEnded()
    {
        beganOverUI = false;
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("TouchMode").Equals("Camera") && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    beganOverUI = true;
                    return;
                }
                else
                {
                    OnTouchBegan(touch);
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!beganOverUI)
                {
                    OnTouchMoved(touch);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                OnTouchEnded();
            }
        }
    }
}
