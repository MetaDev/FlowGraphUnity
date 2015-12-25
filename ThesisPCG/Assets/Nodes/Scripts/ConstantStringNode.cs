using UnityEngine;
using System.Collections;

public class ConstantStringNode : StringNode {
    public string value = "";

    public override string Result() {
        return value;
    }
}
