using System;
using Graph;
using Graph.Parameters;
using UnityEngine;

namespace Generator
{
	public class BlockGenerator : TargetNode
	{
		
		IntegerVector2Parameter position = new IntegerVector2Parameter ("Position");
		ColorParameter color = new ColorParameter ("Color");

		public BlockGenerator () : base ("Block")
		{
			//set input parameters
			AddInputParameter (position);
			color.SetValue (new Color (0.1f, 0.20f, 0.3f));
			AddInputParameter (color);
		}

		public override void Complete ()
		{
			//set position
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (position.GetValue1 (), position.GetValue2 (), 0);
			//set color
			cube.GetComponent<Renderer> ().material.color = color.GetValue ();
		}
		
	}
}

