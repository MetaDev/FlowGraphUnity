using Graph.Parameters;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Graph
{
    //read color map and position, return color
    public class SampleMapColorNode : PropagatorNode
	{
		ColorParameter outColor = new ColorParameter ("Color");
        ColorMapParameter colorMap;

        public SampleMapColorNode (ColorMapParameter colorMap) : base ("Sample Map Color", new IntegerVector2Parameter("Position"))
		{
            this.colorMap = colorMap;
        }

		//read colors from map
		protected override Parameter TransformParameter (Dictionary<String,Parameter> parameters)
		{  
            IntegerVector2Parameter position = parameters["Position"].As<IntegerVector2Parameter>();
            var col = colorMap.GetValue (position.GetValue().Item1, position.GetValue ().Item2);
            outColor = new ColorParameter("Color");
            outColor.SetValue(col);
            return outColor;
		}

        public override Parameter PortParameter()
        {
           return outColor;
        }
    }
}

