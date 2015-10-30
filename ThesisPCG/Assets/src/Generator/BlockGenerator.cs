using System;
using Graph;
using Graph.Parameters;
using UnityEngine;
using System.Collections.Generic;

namespace Generator
{
	public class BlockGenerator : TargetNode
	{
		
		static IntegerVector2Parameter position = new IntegerVector2Parameter ("Position");
		static ColorParameter color = new ColorParameter ("Color");

		public BlockGenerator () : base ("Block", position, color)
		{
			//set input parameters
			color.SetValue (new Color (0.1f, 0.20f, 0.3f));
		}

		protected override void ConsumeParameters (IList<Parameter> parameters)
		{
			//set position
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (position.GetValue1 (), 0, position.GetValue2 ());
			//set color
			cube.GetComponent<Renderer> ().material.color = color.GetValue ();
		}
		
	}
}

