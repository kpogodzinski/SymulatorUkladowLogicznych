using UnityEngine;

public class Clock : Element
{
    [SerializeField]
    private float frequency;

    private void Pulse()
    {
        outputs[0] = !outputs[0];
    }

    private void Start()
    {
        InvokeRepeating(nameof(Pulse), 0f, 1f / frequency / 2f);
    }
}