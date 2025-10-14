using UnityEngine;

public class Switch : Element
{
    public void SetOutput(bool value)
    {
        output = value;
    }

    private void Awake()
    {
        inputCount = 0;
        inputs = null;

        pins = new()
        {
            transform.GetChild(0).gameObject
        };
    }

    private void Update()
    {
        pins[0].GetComponent<SpriteRenderer>().color = GetComponent<SpriteChanger>().GetIndex() == 1 ? Color.green : Color.red;
    }
}
