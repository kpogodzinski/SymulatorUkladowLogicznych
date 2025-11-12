using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private GameObject screenshotInfo;
    private string filename;

    private IEnumerator ShowInfo()
    {
        Image image = screenshotInfo.GetComponent<Image>();
        TextMeshProUGUI text = screenshotInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        screenshotInfo.SetActive(true);
        image.canvasRenderer.SetAlpha(0f);
        text.canvasRenderer.SetAlpha(0f);
        text.text = $"Screenshot saved as {filename.Replace("../", "")}.";
        yield return null;


        float duration = 0.5f;
        image.CrossFadeAlpha(1f, duration, false);
        text.CrossFadeAlpha(1f, duration, false);
        yield return new WaitForSeconds(duration + 2f);
        image.CrossFadeAlpha(0f, duration, false);
        text.CrossFadeAlpha(0f, duration, false);
        yield return new WaitForSeconds(duration);

        screenshotInfo.SetActive(false);
    }

    public void TakeScreenshot()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string name = $"Screenshot_{timestamp}.png";
        filename = Application.platform == RuntimePlatform.Android ?
            $"../../../../DCIM/Screenshots/{name}" : name;
    
        ScreenCapture.CaptureScreenshot(filename, 4);
        StartCoroutine(ShowInfo());
    }
}
