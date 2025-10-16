using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    private Switch sw;

    private void Awake()
    {
        sw = GetComponent<Switch>();
    }

    private void Update()
    {
        if (!PlayerPrefs.GetString("TouchMode").Equals("Interaction"))
            return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Element") && hit.collider.gameObject == this.gameObject)
                {
                    GetComponent<SpriteChanger>().NextSprite();
                    sw.SetOutput(!sw.GetOutput());
                }
            }
        }
    }
}
