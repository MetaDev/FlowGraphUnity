using System;
using Graph.Parameters;
using System.Collections.Generic;

namespace Graph
{
	//read color map and position, return color
	public class SampleMapColorNode : PropagatorNode
	{
		static ColorParameter outColor = new ColorParameter ("Color");
		static IntegerVector2Parameter inPosition = new IntegerVector2Parameter ("Position");
		static ColorMapParameter inColorMap = new ColorMapParameter ("colormap");

		public SampleMapColorNode () : base ("Sample Map Color", outColor, inPosition, inColorMap)
		{
			
		
		}
		//read colors from map
		protected override Parameter TransformParameter (IList<Parameter> inputParameters)
		{
			var col = inColorMap.GetValue () [inPosition.GetValue1 (), inPosition.GetValue2 ()];
			outColor.SetValue (col);
			return outColor;
		}

	}
}

