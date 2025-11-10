using UnityEngine;
using UnityEngine.UI;

public class SetTouchMode : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetString("TouchMode", "Selection");
    }

    public void Set()
    {
        if (GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetString("TouchMode", name.Replace("Toggle", ""));
        }
    }
}
