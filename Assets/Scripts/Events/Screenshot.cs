using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private Popup popup;
    [SerializeField]
    private GameObject canvas;

    private void Awake()
    {
        string directory = $"{Application.persistentDataPath}/Screenshots";
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        foreach (string file in Directory.GetFiles(directory))
            File.Delete(file);
    }

    public void TakeScreenshot()
    {
        StartCoroutine(SaveToGallery());
    }

    private IEnumerator SaveToGallery()
    {
        canvas.GetComponent<Canvas>().enabled = false;
        yield return new WaitForEndOfFrame();

        string filename = $"Screenshot_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.png";
        ScreenCapture.CaptureScreenshot($"Screenshots/{filename}");

        yield return new WaitForEndOfFrame();
        canvas.GetComponent<Canvas>().enabled = true;
        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => File.Exists($"{Application.persistentDataPath}/Screenshots/{filename}"));

        try
        {
            NativeGallery.SaveImageToGallery(
                existingMediaPath: $"{Application.persistentDataPath}/Screenshots/{filename}",
                album: "DigitalCircuit",
                filename: filename,
                callback: (success, path) =>
                {
                    if (success)
                        popup.ShowPopup($"Screenshot saved as: {path}", 3f);
                }
            );
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            popup.ShowPopup("There was an error while saving the screenshot.", 3f);
        }
    }
}