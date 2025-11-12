using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration;

    private void OnEnable()
    {
        GetComponent<Popup>().enabled = true;
    }

    public void ShowPopup(string text, float duration)
    {
        gameObject.SetActive(true);
        StartCoroutine(PopupHandler(text, duration));
    }

    private IEnumerator PopupHandler(string text, float duration)
    {
        Image image = GetComponent<Image>();
        TextMeshProUGUI tmpro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // Setup
        image.canvasRenderer.SetAlpha(0f);
        tmpro.canvasRenderer.SetAlpha(0f);
        tmpro.text = text;
        yield return null;

        // Fade In
        image.CrossFadeAlpha(1f, fadeDuration, false);
        tmpro.CrossFadeAlpha(1f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration + duration);

        // Fade Out
        image.CrossFadeAlpha(0f, fadeDuration, false);
        tmpro.CrossFadeAlpha(0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);

        gameObject.SetActive(false);
    }
}
