using System;
using System.Collections.Generic;
using Graph.Parameters;
using UniRx;
using UnityEngine;

namespace Graph.Propagator
{
    class Scale : PropagatorNode
    {
        private Vector3 scale;
        public Scale(Vector3 scale) : base("Scale", new Vector3fParameter[] { new Vector3fParameter("any") }, new Vector3fParameter("any"))
        {
            this.scale = scale;
        }
        protected override IObservable<Parameter> Propagate(IObservable<IList<Parameter>> observable)
        {
            return observable.Select((parameters) =>
            {
                var vect = parameters[0].As<Vector3fParameter>();
                var test = new Vector3(vect.GetValue().x, vect.GetValue().y, vect.GetValue().z);
                test.Scale(scale);
                return (Parameter)new Vector3fParameter(vect.Name, test);
            });
        }
    }
}
