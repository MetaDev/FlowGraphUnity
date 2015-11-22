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


		public SampleMapColorNode () : base ("Sample Map Color", new IntegerVector2Parameter("Position"), new ColorMapParameter("Colormap",1,1))
		{	
		}

		//read colors from map
		protected override Parameter TransformParameter (IList<Parameter> parameters)
		{  
            IntegerVector2Parameter position = parameters[0].As<IntegerVector2Parameter>();
            ColorMapParameter colorMap = parameters[1].As<ColorMapParameter>();
            var col = colorMap.GetValue () [position.GetValue1 (), position.GetValue2 ()];
            ColorParameter outColor = new ColorParameter("Color");
            outColor.SetValue (col);
			return outColor;
		}

        public override Parameter GetOutputParameterType()
        {
           return outColor;
        }
    }
}

