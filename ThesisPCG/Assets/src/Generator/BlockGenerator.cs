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
			InputParameters.Add ("Position", new IntegerVector2Parameter ("Position"));
			var color = new ColorParameter ("Color");
			color.SetValue (new Color (0.1f, 0.20f, 0.3f));
			Debug.Log (color.GetValue<Color> ());
			InputParameters ["Color"] = color;
			Debug.Log (InputParameters ["Color"].GetValue<Color> ());
		}

		public override void Complete ()
		{
			//set position
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			IntegerVector2Parameter position = InputParameters ["Position"].As<IntegerVector2Parameter> ();
			cube.transform.position = new Vector3 (position.GetValue1 (), position.GetValue2 (), 0);
			//set color
			ColorParameter color = InputParameters ["Color"].As<ColorParameter> ();
			cube.GetComponent<Renderer> ().material.color = color.GetValue ();
		}
		
	}
}

