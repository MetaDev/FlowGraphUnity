using System;
using Graph;
using Graph.Parameters;
using UnityEngine;
using System.Collections.Generic;

namespace Generator
{
	public class BlockGenerator : TargetNode
	{
		
		
		public BlockGenerator () : base ("Block", new IntegerVector2Parameter("Position"), new ColorParameter("Color"))
		{
		
		}

		protected override void ConsumeParameters (Dictionary<String,Parameter> parameters)
		{
            //
            IntegerVector2Parameter position = parameters["Position"].As< IntegerVector2Parameter>();
            ColorParameter color = parameters["Color"].As<ColorParameter>();
            //set position
            GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (position.GetValue ().Item1, 0, position.GetValue().Item2);
            //set color
            //Debug.Log(color.GetValue());
			cube.GetComponent<Renderer> ().material.color = color.GetValue ();
            
		}
		
	}
}

