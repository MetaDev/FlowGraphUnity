using Graph.Parameters;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Graph.Propagator
{
    class Filter : PropagatorNode
    {
        private float Min;
        private string ToFilterParameterName;
        public Filter(string toFilterParameterName, float min) : base("Filter", new Parameter[] { new Vector3fParameter(toFilterParameterName), new DoubleParameter("Bound") }, new Vector3fParameter(toFilterParameterName))
        {
            this.Min = min;
            this.ToFilterParameterName= toFilterParameterName;
        }
        protected override IObservable<Parameter> Propagate(IObservable<IList<Parameter>> observable)
        {
            return observable.Where((parameters) =>
            {
                var bound = TargetNode.GetParameterFromList("Bound", parameters).As<DoubleParameter>().GetValue();
                
                return bound < Min;
            }).Select((parameters) =>
            {
                var input = TargetNode.GetParameterFromList(ToFilterParameterName, parameters).As<Vector3fParameter>().GetValue();
                var filtered = new Vector3fParameter(ToFilterParameterName, new Vector3(input.x,input.y,input.z));
                return (Parameter)filtered;

            });
        }
    }
}
