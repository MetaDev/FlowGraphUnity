using UnityEngine;
using System.Collections.Generic;
using System;

public class MultiplyIntNode : IntNode {
    public List<IntNode> inputs = new List<IntNode>();

    public override int Result() {
        var result = 1;
        for(int i = 0; i < inputs.Count; i++) {
            result *= inputs[i].Result();
        }
        return result;
    }
}
