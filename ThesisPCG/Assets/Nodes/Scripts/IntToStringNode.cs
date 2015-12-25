using UnityEngine;
using System.Collections;
using System;

public class IntToStringNode : StringNode {
    public IntNode input;

    public override string Result() {
        return input.Result().ToString();
    }
}
