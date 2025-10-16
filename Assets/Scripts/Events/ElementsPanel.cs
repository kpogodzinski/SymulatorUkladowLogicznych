using System.Collections;
using TMPro;
using UnityEngine;

public class ElementsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject elementsPanel;
    [SerializeField]
    private Canvas canvas;
    private bool visible;
    private float animationLength;
    private float width;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        visible = false;
        animationLength = 0.3f;

        buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        Vector3[] corners = new Vector3[4];
        RectTransform rt = elementsPanel.GetComponent<RectTransform>();
        rt.GetWorldCorners(corners);
        width = Vector3.Distance(corners[2], corners[1]) * 1.2f;
    }

    private IEnumerator SlidePanel(int direction) // direction: -1 = left, 1 = right
    {
        Vector2 oldButtonPosition = transform.position;
        Quaternion oldTextRotation = buttonText.transform.rotation;
        Vector2 oldPanelPosition = elementsPanel.transform.position;
        

        Vector2 newButtonPosition = oldButtonPosition + Vector2.right * width * direction;
        Quaternion newTextRotation = oldTextRotation * Quaternion.Euler(0, 0, -direction * 45);
        Vector2 newPanelPosition = oldPanelPosition + Vector2.right * width * direction;

        float elapsedTime = 0;
        while (elapsedTime < animationLength)
        {
            transform.position = Vector2.Lerp(oldButtonPosition, newButtonPosition, elapsedTime / animationLength);
            elementsPanel.transform.position = Vector2.Lerp(oldPanelPosition, newPanelPosition, elapsedTime / animationLength);
            buttonText.transform.rotation = Quaternion.Lerp(oldTextRotation, newTextRotation, elapsedTime / animationLength);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = newButtonPosition;
        elementsPanel.transform.position = newPanelPosition;
        buttonText.transform.rotation = newTextRotation;
        //yield return null;
        visible = !visible;
    }

    public void OnButtonClick()
    {
        if (elementsPanel == null)
            return;

        StartCoroutine(SlidePanel(visible ? 1 : -1));
    }
}
