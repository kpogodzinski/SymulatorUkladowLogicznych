using UnityEngine;

public class Const : Element
{
    [SerializeField]
    private bool signal;

    private void Start()
    {
        outputs[0] = signal;
        pins[0].GetComponent<SpriteRenderer>().color = signal ? Color.green : Color.red;
    }
}