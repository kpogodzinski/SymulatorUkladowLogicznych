using UnityEngine;
using UnityEngine.UI;

public class ScrollVisibility : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform viewport;

    private void Start()
    {
        UpdateVisibility();
    }

    public void UpdateVisibility()
    {
        foreach (Transform category in scrollRect.content)
        {
            foreach (Transform row in category)
            {
                foreach (Transform item in row)
                {
                    var rect = item.GetComponent<RectTransform>();
                    var collider = item.GetComponent<Collider2D>();

                    if (!rect || !collider) continue;

                    bool visible = RectTransformUtility.RectangleContainsScreenPoint(viewport, rect.position);
                    collider.enabled = visible;
                }
            }
        }
    }
}
