using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    private Dictionary<int, GameObject> buttons;

    private void Awake()
    {
        buttons = new();
    }

    private void OnTouching(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Element") && hit.collider.GetComponent<Button>() != null)
        {
            GameObject button = hit.collider.gameObject;
            if (!buttons.ContainsKey(touch.fingerId) || buttons[touch.fingerId] != button)
            {
                if (buttons.ContainsKey(touch.fingerId))
                {
                    GameObject oldButton = buttons[touch.fingerId];
                    oldButton.GetComponent<SpriteChanger>().SetSprite(0);
                    oldButton.GetComponent<Button>().SetOutput(false);
                }
                buttons[touch.fingerId] = button;
                button.GetComponent<SpriteChanger>().SetSprite(1);
                button.GetComponent<Button>().SetOutput(true);
            }
        }
        else
        {
            if (buttons.ContainsKey(touch.fingerId))
            {
                GameObject oldButton = buttons[touch.fingerId];
                oldButton.GetComponent<SpriteChanger>().SetSprite(0);
                oldButton.GetComponent<Button>().SetOutput(false);
                buttons.Remove(touch.fingerId);
            }
        }
    }

    private void OnTouchEnded(Touch touch)
    {
        if (buttons.ContainsKey(touch.fingerId))
        {
            GameObject oldButton = buttons[touch.fingerId];
            oldButton.GetComponent<SpriteChanger>().SetSprite(0);
            oldButton.GetComponent<Button>().SetOutput(false);
            buttons.Remove(touch.fingerId);
        }
    }

    private void Update()
    {
        if (!PlayerPrefs.GetString("TouchMode").Equals("Interaction"))
            return;

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    OnTouching(touch);
                }
                else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    OnTouchEnded(touch);
                }
            }
        }
    }
}
