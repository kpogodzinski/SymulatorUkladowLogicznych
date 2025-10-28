using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        int fps = (int)(Time.frameCount / Time.time);
        GetComponent<TextMeshProUGUI>().text = $"FPS: {fps}";
    }
}
