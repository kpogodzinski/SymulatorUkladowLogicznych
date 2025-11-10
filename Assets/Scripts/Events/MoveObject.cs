using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;
    [SerializeField]
    private Sprite squareSprite;
    private GameObject square;
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

            if (hit.collider.gameObject.CompareTag("Element") || hit.collider.gameObject.CompareTag("ExternalPin"))
            {
                if (!PlayerPrefs.GetString("TouchMode").Equals("Selection"))
                    return;

                touchedObject = hit.collider.gameObject;
                Vector2 objectPosition = touchedObject.transform.position;
                offset = objectPosition - touchPosition;

                square = new GameObject("Square", typeof(SpriteRenderer));
                square.transform.parent = touchedObject.transform;
                square.transform.localPosition = Vector3.zero;
                square.transform.localScale = Vector3.one;

                SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
                sr.sprite = squareSprite;
                sr.sortingOrder = -1;
                sr.color = new Color32(0x70, 0x5E, 0x46, 0xBF);
                sr.drawMode = SpriteDrawMode.Sliced;
                sr.size = touchedObject.CompareTag("ExternalPin") ? 
                    Vector2.one * 2.56f : touchedObject.GetComponent<RectTransform>().rect.size;
            }
            else if (hit.collider.gameObject.CompareTag("NewElement"))
            {
                scrollRect.enabled = false;
                touchedObject = Instantiate(hit.collider.gameObject, touchPosition, Quaternion.identity, GameObject.Find("Canvas").transform);
                touchedObject.GetComponent<RectTransform>().sizeDelta = new Vector2(512f, 512f);
                touchedObject.transform.localScale = 0.5f * 1.2f * Vector3.one;
                touchedObject.GetComponent<Image>().color -= new Color(0, 0, 0, 0.5f);
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
            touchedObject.GetComponent<Image>().color += new Color(0, 0, 0, 0.5f);
            touchedObject.GetComponent<SpawnObject>().Spawn(touchedObject.transform.position);
            Destroy(touchedObject);
            scrollRect.enabled = true;
        }

        Destroy(square);
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
