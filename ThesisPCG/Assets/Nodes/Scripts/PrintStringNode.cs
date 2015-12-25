using UnityEngine;
using System.Collections;
using System;

public class PrintStringNode : NullNode {
    public StringNode input;

    public override object Result() {
        Debug.Log(input.Result());
        return null;
    }

    void Start() {
        Result();
    }
}
