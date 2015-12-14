using Graph.Parameters;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

namespace Graph
{
    //read color map and position, return color
    public class SampleMapColorNode : PropagatorNode
    {
        Color[,] ColorMap;

        public SampleMapColorNode(Color[,] colorMap) : base("Sample Map Color", new Parameter[] { new Vector3fParameter("Position") }, new ColorParameter("Color"))
        {
            this.ColorMap = colorMap;
        }

        //read colors from map
        protected override IObservable<Parameter> Propagate(IObservable<IList<Parameter>> observable)
        {
            return observable.Select((parameters) =>
            {

                Vector3fParameter position = TargetNode.GetParameterFromList("Position", parameters).As<Vector3fParameter>();
                var col = ColorMap[(int)(position.GetValue().x * ColorMap.GetLength(0)), (int)(position.GetValue().z * ColorMap.GetLength(1))];
                Parameter outColor = new ColorParameter(this.OutputParameter().Name, col);
                return outColor;
            });

        }



    }
}

