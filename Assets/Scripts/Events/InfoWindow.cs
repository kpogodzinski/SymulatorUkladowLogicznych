using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject infoWindow;
    private Toggle toggle;
    private string version;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        version = Application.version;
        foreach (Transform child in infoWindow.transform)
        {
            if (child.name.Equals("Version"))
            {
                child.GetChild(1).GetComponent<TextMeshProUGUI>().text = version;
            }
        }
    }

    public void ToggleWindow()
    {
        infoWindow.SetActive(toggle.isOn);
    }
}
