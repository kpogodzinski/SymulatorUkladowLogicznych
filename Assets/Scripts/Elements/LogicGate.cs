using System.Collections.Generic;
using UnityEngine;

public abstract class LogicGate : Element
{
    public abstract bool Evaluate();

    protected List<GameObject> GetChildren()
    {
        List<GameObject> list = new();
        for (int i = 0; i < transform.childCount; i++)
            list.Add(transform.GetChild(i).gameObject);
        return list;
    }

    protected void Awake()
    {
        inputs = new(2) { false, false };
        pins = GetChildren();
    }

    protected void Update()
    {
        output = Evaluate();

        pins[0].GetComponent<SpriteRenderer>().color = inputs[0] ? Color.green : Color.red;
        pins[1].GetComponent<SpriteRenderer>().color = inputs[1] ? Color.green : Color.red;
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
