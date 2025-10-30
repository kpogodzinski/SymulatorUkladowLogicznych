using UnityEngine;

public class CutWire : MonoBehaviour
{
    [SerializeField]
    private GameObject pinPrefab;


    private void OnTouchBegan(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Wire"))
        {
            Wire wire = hit.collider.GetComponent<Wire>();
            GameObject pin = Instantiate(pinPrefab, (Vector2)Camera.main.ScreenToWorldPoint(touch.position), Quaternion.identity, wire.transform.parent);
            pin.tag = "ExternalPin";
            pin.transform.localScale /= 2f;
            pin.AddComponent<Pin>();

            Wire wire1 = Instantiate(wire, wire.transform.parent);
            wire1.SetSource(wire.GetSource());
            wire1.SetSourceIndex(wire.GetSourceIndex());
            wire1.SetTarget(pin);
            wire1.SetTargetIndex(-1);
            wire1.SetConnected(true);

            Wire wire2 = Instantiate(wire, wire.transform.parent);
            wire2.SetSource(pin);
            wire2.SetSourceIndex(-1);
            wire2.SetTarget(wire.GetTarget());
            wire2.SetTargetIndex(wire.GetTargetIndex());
            wire2.SetConnected(true);

            wire.transform.parent = pin.transform;
            wire.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!PlayerPrefs.GetString("TouchMode").Equals("Wiring"))
            return;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                OnTouchBegan(touch);
        }
    }
}
