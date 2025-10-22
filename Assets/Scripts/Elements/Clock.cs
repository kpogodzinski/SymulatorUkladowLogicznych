using System.Collections;
using UnityEngine;

public class Clock : Element
{
    [SerializeField]
    private float frequency;

    private void Pulse()
    {
        output = !output;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Pulse), 0f, frequency / 2f);
    }

    private void Update()
    {
        pins[0].GetComponent<SpriteRenderer>().color = output ? Color.green : Color.red;
    }
}
