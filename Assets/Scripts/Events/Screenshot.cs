using System;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private Popup popup;

    public void TakeScreenshot()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string name = $"Screenshot_{timestamp}.png";
        string filename = Application.platform == RuntimePlatform.Android ?
            $"../../../../DCIM/Screenshots/{name}" : name;
    
        ScreenCapture.CaptureScreenshot(filename, 4);
        popup.ShowPopup($"Screenshot saved as: {filename.Replace("../", "")}.", 2f);
    }
}
