using UnityEngine;
using System.Collections.Generic;
using System;

public class ConcatenateStringNode : StringNode {
    public List<StringNode> inputs = new List<StringNode>();

    public override string Result() {
        var result = "";
        for (int i = 0; i < inputs.Count; i++) {
            result += inputs[i].Result();
        }
        return result;
    }
}
