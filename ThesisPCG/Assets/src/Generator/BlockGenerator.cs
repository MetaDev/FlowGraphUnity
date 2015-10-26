using System;
using Graph;
using Graph.Parameters;
using UnityEngine;

namespace Generator
{
	public class BlockGenerator : TargetNode
	{



		public BlockGenerator () : base ("Block")
		{
			//set input parameters
			AddInputParameter (new IntegerVector2Parameter ("Position"));
			AddInputParameter (new ColorParameter ("Position"));

		}

		public override void Complete ()
		{
			//set color
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			IntegerVector2Parameter position = GetInputParameter<IntegerVector2Parameter> ("Position");
			cube.transform.position = new Vector3 (position.GetValue1 (), position.GetValue2 (), 0);
			//set position
			ColorParameter color = GetInputParameter<ColorParameter> ("Position");
			cube.GetComponent<Renderer> ().material.color = color.GetValue ();
		}
		
	}
}

