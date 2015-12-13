using Graph.Parameters;
using System;
using UnityEngine;

namespace Graph.Parameters
{
    class Vector3fParameter : Parameter, IParameter<Vector3>
    {
        public Vector3fParameter(string name, Vector3 value = default(Vector3)) : base(name,value)
        {
        }

        
        public Vector3 GetValue()
        {
            return this.GetValue<Vector3>();
        }
    }
}
