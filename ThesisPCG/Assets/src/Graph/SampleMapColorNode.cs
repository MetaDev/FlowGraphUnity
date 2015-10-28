//using System;
//using Graph.Parameters;
//
//namespace Graph
//{
//	//read color map and position, return color
//	public class SampleMapColorNode : PropagatorNode 
//	{
//		public SampleMapColorNode () : base ("Sample Map Color",new ColorParameter("Color"))
//		{
//			//set input parameters
//			Parameter inputMap = new ColorMapParameter ("Colormap");
//			this.AddInputParameter(inputMap);
//			Parameter position = new IntegerVector2Parameter ("Position");
//			this.AddInputParameter(position);
//		
//		}
//		//read colors from map
//		protected override void Transform ()
//		{
//			var pos = this.GetInputParameter<IntegerVector2Parameter> ("Position").GetValue ();
//			var map = this.GetInputParameter <ColorMapParameter>("Colormap").GetValue ();
//			var col = map[pos.Item1,pos.Item2];
//			GetOutputParameter<ColorParameter> ().SetValue (col);
//		}
//
//	}
//}
//
