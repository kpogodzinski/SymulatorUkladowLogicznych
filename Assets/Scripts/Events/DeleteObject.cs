using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    private void OnTouchBegan(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) 
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("Element") || go.CompareTag("Wire"))
                Destroy(go);
        }
    }

    private void Update()
    {
        if (!PlayerPrefs.GetString("TouchMode").Equals("Delete"))
            return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                OnTouchBegan(touch);
        }
    }
}
