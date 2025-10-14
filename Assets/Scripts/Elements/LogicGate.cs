using System.Collections.Generic;
using UnityEngine;

public abstract class LogicGate : Element
{
    public abstract bool Evaluate();

    protected void Update()
    {
        output = Evaluate();

        for (int i = 0; i < pins.Count - 1; i++)
            pins[i].GetComponent<SpriteRenderer>().color = inputs[i] ? Color.green : Color.red;

        pins[^1].GetComponent<SpriteRenderer>().color = output ? Color.green : Color.red;


        /////// TESTING PURPOSES /////
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    inputs[0] ^= true;
        //    //Debug.Log("1");
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    inputs[1] ^= true;
        //    //Debug.Log("2");
        //}
        //////////////////////////////
    }
}
