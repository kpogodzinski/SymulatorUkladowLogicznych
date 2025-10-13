using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AndGate : LogicGate
{
    public override bool Evaluate()
    {
        if (inputs.Any(a => !a))
            return false;
        return true;
    }

    private void Awake()
    {
        inputs = new(2) {false, false};
        pins = GetChildren();
    }

    private void Update()
    {
        output = Evaluate();

        pins[0].GetComponent<SpriteRenderer>().color = inputs[0] ? Color.green : Color.red;
        pins[1].GetComponent<SpriteRenderer>().color = inputs[1] ? Color.green : Color.red;
        pins[^1].GetComponent<SpriteRenderer>().color = output ? Color.green : Color.red;


        ///// TESTING PURPOSES /////
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inputs[0] ^= true;
            Debug.Log("1");
        }
        if (Input.GetKeyDown(KeyCode.W))
        { 
            inputs[1] ^= true;
            Debug.Log("2");
        }
        ////////////////////////////
    }
}
