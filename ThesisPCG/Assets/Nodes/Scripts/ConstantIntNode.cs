using UnityEngine;
using System.Collections;
using System;

public class ConstantIntNode : IntNode {
    public int value = 0;

    public override int Result() {
        return value;
    }
}
